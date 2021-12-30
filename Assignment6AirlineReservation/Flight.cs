using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// This class represents flights in the database
    /// </summary>
    class Flight
    {
        /// <summary>
        /// These represent the 3 columns in the flight table 
        /// </summary>
        int flight_id;
        int flight_number;
        string aircraft_type;

        /// <summary>
        /// If a flight is created but it is given no values give it these default values
        /// </summary>
        Flight()
        {
            flight_id = 0;
            flight_number = 0;
            string aircraft_type = "";
        }
        /// <summary>
        /// Creates a flight with a flight_id, flight_number and aircraft_type.  Will be used by the business logic class
        /// when it looks at the flight table in the database
        /// </summary>
        /// <param name="flightid"></param>
        /// <param name="flightnumber"></param>
        /// <param name="aircrafttype"></param>
        public Flight(int flightid, int flightnumber, string aircrafttype)
        {
            flight_id = flightid;
            flight_number = flightnumber;
            aircraft_type = aircrafttype;
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
        /// Returns flight_number
        /// </summary>
        /// <returns></returns>
        public int get_flight_number()
        {
            return flight_number;
        }
        /// <summary>
        /// Returns aircraft_type
        /// </summary>
        /// <returns></returns>
        public string get_aircraft_type()
        {
            return aircraft_type;
        }
        /// <summary>
        /// Sets flight_id
        /// </summary>
        /// <param name="flightid"></param>
        public void set_flight_id(int flightid)
        {
            flight_id = flightid;
            if (flight_id < 1)
                throw new invalid_number("Flight ID can't be less than 1");
        }
        /// <summary>
        /// Sets flight_number
        /// </summary>
        /// <param name="flightnumber"></param>
        public void set_flight_number(int flightnumber)
        {
            flight_number = flightnumber;
            if (flight_number < 1)
                throw new invalid_number("Flight number can't be less than 1");
        }
        /// <summary>
        /// Sets aircraft_type
        /// </summary>
        /// <param name="aircrafttype"></param>
        public void set_aircraft_type(string aircrafttype)
        {
            aircraft_type = aircrafttype;
        }



        public class invalid_number : System.Exception
        {
            public invalid_number() : base() { }
            public invalid_number(string message) : base(message) { }
            public invalid_number(string message, System.Exception inner) : base(message, inner) { }
        }
    }
}
