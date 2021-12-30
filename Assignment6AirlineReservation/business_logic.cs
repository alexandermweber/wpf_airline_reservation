using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// This class will use the clsDataAccess class to get data from the database and return it to 
    /// the UI so the UI doesn't have to deal with the database itself
    /// </summary>
    class business_logic
    {
        /// <summary>
        /// We will use this clsDataAccess object to access the data in the ReservationSystem database
        /// </summary>
        clsDataAccess database_access;//Using the class we were given in assignment 6 help
        public business_logic()
        {
            database_access = new clsDataAccess();
        }

        /// <summary>
        /// Returns a string array containing the flight number and aircraft type of the flights in the database.
        /// These strings will be the content of the choose_flight_combobox combobox
        /// </summary>
        /// <returns></returns>
        public string[] get_flights()
        {
            try
            {
                DataSet data;
                int number_of_return_values = 0;
                string sql_query = "SELECT Flight_Number, Aircraft_Type FROM Flight";
                data = database_access.ExecuteSQLStatement(sql_query, ref number_of_return_values);

                string[] a = new string[data.Tables[0].Rows.Count];
                for(int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    a[i] = data.Tables[0].Rows[i][0].ToString() + " " + data.Tables[0].Rows[i][1].ToString();
                }
                return a;
            }
            catch
            {
                throw new database_exception("Something went wrong in get_flights");
            }
        }

        /// <summary>
        /// Returns an array of strings of the first and last names of the passengers of the flight that 
        /// has the given flight_id.  These strings will be the content of the choose_passenger_combobox.
        /// </summary>
        /// <param name="flightid"></param>
        /// <returns></returns>
        public string[] get_passengers(int flightid)
        {
            try
            {
                flightid++;//The int the UI will pass will be the index of the combobox selection.
                //The items in the combobox should be in order of flight id, but because the flight id starts at 1
                //and the combobox index starts at 0 I will need to add 1 to flightid before I use it.
                DataSet data;
                int number_of_return_values = 0;
                string sql_query = 
                    string.Format("SELECT First_Name, Last_Name FROM Passenger INNER JOIN Flight_Passenger_Link ON " +
                    "Passenger.Passenger_ID = Flight_Passenger_Link.Passenger_ID WHERE Flight_ID = {0}", flightid);
                data = database_access.ExecuteSQLStatement(sql_query, ref number_of_return_values);

                string[] a = new string[data.Tables[0].Rows.Count];
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    a[i] = data.Tables[0].Rows[i][0].ToString() + " " + data.Tables[0].Rows[i][1].ToString();
                }
                return a;
            }
            catch
            {
                throw new database_exception("There wan an error in get_passengers");
            }

           
        }

        /// <summary>
        /// Returns the aircraft_type of the flight with the given flight_id.
        /// This string will be the content of the label on the display.
        /// </summary>
        /// <returns></returns>
        public string get_flight_name(int flightid)
        {
            try
            {

                DataSet data;
                int number_of_return_values = 0;
                string sql_query = string.Format("SELECT Aircraft_Type FROM Flight WHERE Flight_ID = {0}", flightid);
                data = database_access.ExecuteSQLStatement(sql_query, ref number_of_return_values);

                return (data.Tables[0].Rows[0][0].ToString());
            }
            catch
            {
                throw new database_exception("Something went wrong in get_first_flight_name");
            }
        }

     

        /// <summary>
        /// Returns a list of passengers
        /// </summary>
        /// <returns></returns>
        public List<Passenger> get_passengers_list()
        {
            try
            {
                DataSet data;
                int number_of_return_values = 0;
                string sql_query =
                    "SELECT Passenger.Passenger_ID, First_Name, Last_Name, Seat_Number, Flight_ID FROM Passenger " +
                    "INNER JOIN Flight_Passenger_Link ON Passenger.Passenger_ID = Flight_Passenger_Link.Passenger_ID";
                data = database_access.ExecuteSQLStatement(sql_query, ref number_of_return_values);

                List<Passenger> passengers = new List<Passenger>();
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    passengers.Add(new Passenger(Int32.Parse(data.Tables[0].Rows[i][0].ToString()),
                        data.Tables[0].Rows[i][1].ToString(), data.Tables[0].Rows[i][2].ToString(),
                        Int32.Parse(data.Tables[0].Rows[i][3].ToString()), Int32.Parse(data.Tables[0].Rows[i][4].ToString())));
                }
                return passengers;
            }
            catch
            {
                throw new database_exception("There was an error creating a list of passengers");
            }
        }

        /// <summary>
        /// Returns a list of flights
        /// </summary>
        /// <returns></returns>
        public List<Flight> get_flights_list()
        {
            try
            {
                DataSet data;
                int number_of_return_values = 0;
                string sql_query =
                    "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM Flight";
                data = database_access.ExecuteSQLStatement(sql_query, ref number_of_return_values);

                List<Flight> flights = new List<Flight>();
                for (int i = 0; i < data.Tables[0].Rows.Count; i++)
                {
                    flights.Add(new Flight(Int32.Parse(data.Tables[0].Rows[i][0].ToString()),
                        Int32.Parse(data.Tables[0].Rows[i][1].ToString()), data.Tables[0].Rows[i][2].ToString()));
                }
                return flights;
            }
            catch
            {
                throw new database_exception("There was an error creating a list of passengers");
            }
        }

        public void add_passenger(string first_name, string last_name)
        {
            try
            {
                string sql_query = string.Format("INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('{0}','{1}')",
                    first_name, last_name);
                database_access.ExecuteNonQuery(sql_query);

                string passenger_id = get_passenger_id(first_name, last_name);


               
            }
            catch
            {
                throw new database_exception("There was an error adding a passenger");
            }
        }

        public string get_passenger_id(string first_name, string last_name)
        {
            DataSet data;
            int number_of_return_values = 0;
            string sql_query =
                string.Format("SELECT Passenger_ID from Passenger where First_Name = '{0}' AND Last_Name = '{1}'",
                first_name, last_name);
            data = database_access.ExecuteSQLStatement(sql_query, ref number_of_return_values);
            return data.Tables[0].Rows[0][0].ToString();
        }


        public void insert_into_link_table(int flight_id, string first_name, string last_name, int seat_number)
        {
            string passenger_id = get_passenger_id(first_name, last_name);
            string sql_query =
                 string.Format("INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) VALUES ({0}, {1}, {2})", 
                 flight_id, passenger_id, seat_number);
            database_access.ExecuteNonQuery(sql_query);
        }

        public void update_seat(int flight_id, string first_name, string last_name, int seat_number)
        {
            string passenger_id = get_passenger_id(first_name, last_name);
            string sql_query =
                 string.Format("UPDATE FLIGHT_PASSENGER_LINK SET Seat_Number = {0} WHERE FLIGHT_ID = {1} AND PASSENGER_ID = {2}",
                 seat_number, flight_id, passenger_id);
            
            database_access.ExecuteNonQuery(sql_query);
        }


        public void delete_passenger(string first_name, string last_name)
        {
            try
            {
                string passenger_id = get_passenger_id(first_name, last_name);
                string sql_query =
                    string.Format("DELETE FROM FLIGHT_PASSENGER_LINK WHERE PASSENGER_ID = {0}", passenger_id);
                database_access.ExecuteNonQuery(sql_query);
            }
            catch
            {
                throw new database_exception("There was an error deleting a passenger link");
            }

            try
            {
                string passenger_id = get_passenger_id(first_name, last_name);
                string sql_query =
                    string.Format("DELETE FROM PASSENGER WHERE PASSENGER_ID = {0}", passenger_id);
                database_access.ExecuteNonQuery(sql_query);
            }

            catch
            {
                throw new database_exception("There was an error deleting a passenger");
            }
        }
    }


    public class database_exception : System.Exception
    {
        public database_exception() : base() { }
        public database_exception(string message) : base(message) { }
        public database_exception(string message, System.Exception inner) : base(message, inner) { }
    }

   

}
