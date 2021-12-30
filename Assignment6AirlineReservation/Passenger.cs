using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Represents passengers from the database.  
    /// This will have data from the Passenger table and the Flight_Passenger_Link table
    /// </summary>
    class Passenger
    {
        /// <summary>
        /// These represent the 3 columns in the passenger table
        /// </summary>
        int passenger_id;
        string first_name;
        string last_name;  
        
        /// <summary>
        /// These represent 2 more columns in the flight_passenger_link table
        /// </summary>
        int seat_number;
        int flight_id;

        /// <summary>
        /// This is not in the database.  This will just be the first_name and the last_name.
        /// I added this because I might have to display both the first name and last name of 
        /// a passenger a lot and I don't want to have to type first_name + " " + last_name every time
        /// </summary>
        string full_name;

        /// <summary>
        /// When I change the passenger's seat, I will need to know if they were already in the database
        /// or if this is a new passenger.
        /// </summary>
        bool new_passenger = false;

        /// <summary>
        /// If a passenger is created and no values are given give it these default values
        /// </summary>
        public Passenger()
        {
            passenger_id = 0;
            first_name = "";
            last_name = "";
            seat_number = 0;
            flight_id = 0;
            full_name = "";
            new_passenger = false;
        }

        /// <summary>
        /// A passenger might be created with data from the Passenger table but no data for any flight.
        /// If that happens use the data we are given and set the rest to these default values;
        /// </summary>
        /// <param name="passengerid"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        public Passenger(int passengerid, string firstname, string lastname)
        {
            passenger_id = passengerid;
            first_name = firstname;
            last_name = lastname;
            seat_number = 0;
            flight_id = 0;
            full_name = first_name + " " + last_name;
            new_passenger = false;
        }

        /// <summary>
        /// One more constructor for when we are given all the data we need
        /// </summary>
        /// <param name="passengerid"></param>
        /// <param name="firstname"></param>
        /// <param name="lastname"></param>
        /// <param name="seatnumber"></param>
        /// <param name="flightid"></param>
        public Passenger(int passengerid, string firstname, string lastname, int seatnumber, int flightid)
        {
            passenger_id = passengerid;
            first_name = firstname;
            last_name = lastname;
            seat_number = seatnumber;
            flight_id = flightid;
            full_name = first_name + " " + last_name;
            new_passenger = false;
        }

        /// <summary>
        /// Returns passenger_id
        /// </summary>
        /// <returns></returns>
        public int get_passenger_id()
        {
            return passenger_id;
        }
        /// <summary>
        /// Returns first_name
        /// </summary>
        /// <returns></returns>
        public string get_first_name()
        {
            return first_name;
        }
        /// <summary>
        /// Returns last_name
        /// </summary>
        /// <returns></returns>
        public string get_last_name()
        {
            return last_name;
        }
        /// <summary>
        /// Returns seat_number
        /// </summary>
        /// <returns></returns>
        public int get_seat_number()
        {
            return seat_number;
        }
        /// <summary>
        /// Returns flight_id
        /// </summary>
        /// <returns></returns>
        public int get_flight_id()
        {
            return flight_id;
        }
        /// <summary>
        /// Returns full_name
        /// </summary>
        /// <returns></returns>
        public string get_full_name()
        {
            return full_name;
        }
        /// <summary>
        /// Sets passenger_id
        /// </summary>
        /// <param name="passengerid"></param>
        public void set_passenger_id(int passengerid)
        {
            passenger_id = passengerid;
        }
        /// <summary>
        /// Sets first_name
        /// </summary>
        /// <param name="firstname"></param>
        public void set_first_name(string firstname)
        {
            first_name = firstname;
            update_full_name();
        }
        /// <summary>
        /// Sets last_name
        /// </summary>
        /// <param name="lastname"></param>
        public void set_last_name(string lastname)
        {
            last_name = lastname;
            update_full_name();
        }
        /// <summary>
        /// Sets seat_number
        /// </summary>
        /// <param name="seatnumber"></param>
        public void set_seat_number(int seatnumber)
        {
            seat_number = seatnumber;
        }
        /// <summary>
        /// Sets flight_id
        /// </summary>
        /// <param name="flightid"></param>
        public void set_flight_id(int flightid)
        {
            flight_id = flightid;
        }
        /// <summary>
        /// Combines first_name and last_name to create full_name.
        /// This will be called whenever first_name or last_name changes.
        /// </summary>
        private void update_full_name()
        {
            full_name = first_name + " " + last_name;
        }


        public bool get_new_passenger()
        {
            return new_passenger;
        }

        public void set_new_passenger(bool is_new_passenger)
        {
            new_passenger = is_new_passenger;
        }
    }
}
