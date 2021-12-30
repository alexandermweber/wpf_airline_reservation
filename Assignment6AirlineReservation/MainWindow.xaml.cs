using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //clsDataAccess clsData;
        wndAddPassenger wndAddPass;

        business_logic business_Logic;
        List<Passenger> passengers;
        List<Flight> flights;
        Label[] first_flight_seats;
        Label[] second_flight_seats;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
               
                business_Logic = new business_logic();
                passengers = business_Logic.get_passengers_list();
                flights = business_Logic.get_flights_list();
                first_flight_seats = create_first_array();
                second_flight_seats = create_second_array();

                set_choose_flight_combobox();
                
                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string selection = cbChooseFlight.SelectedItem.ToString();
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;
               

                if (cbChooseFlight.SelectedIndex == 0)
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                    update_first_flight();//This function will show what seats are available
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                    update_second_flight();
                }
                
                cbChoosePassenger.Items.Clear();


                string[] passengers = business_Logic.get_passengers(cbChooseFlight.SelectedIndex);
                //Would be nice if code from another class executed the SQL above, added each passenger into a Passenger object, then into a list of Passengers to be returned and bound to the combo box
                for (int i = 0; i < passengers.Length; i++)
                {
                 
                    cbChoosePassenger.Items.Add(passengers[i]);
                }

            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();
                //passengers.Add(wndAddPass.txtFirstName
                Passenger new_passenger = new Passenger(wndAddPass.passenger_id, wndAddPass.txtFirstName.Text, wndAddPass.txtLastName.Text);
                new_passenger.set_flight_id((cbChooseFlight.SelectedIndex + 1));
                //This passenger has not been added to the Flight_Passenger_Link table yet
                new_passenger.set_new_passenger(true);
                
                passengers.Add(new_passenger);
                cbChoosePassenger.Items.Add(passengers[passengers.Count - 1].get_full_name());
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }


        /// <summary>
        /// Create an array of seats in the first flight
        /// </summary>
        /// <returns></returns>
        private Label[] create_first_array()
        {
            Label[] label_array = { Seat1, Seat2, Seat3, Seat4, Seat5, Seat6, Seat7,
            Seat8, Seat9, Seat10, Seat11, Seat12, Seat13, Seat14, Seat15, Seat16};
            return label_array;
        }
        /// <summary>
        /// Create an array of seats in the second flight
        /// </summary>
        /// <returns></returns>
        private Label[] create_second_array()
        {
            Label[] label_array = { SeatA1, SeatA2, SeatA3, SeatA4, SeatA5, 
                SeatA6, SeatA7, SeatA8, SeatA9, SeatA10, SeatA11, SeatA12, 
                SeatA13, SeatA14, SeatA15};
            return label_array;
        }




        /// <summary>
        /// This gets the aircraft_type from the flights in the database through 
        /// the businesslogic class, and then makes that string the content of the 
        /// cbChooseFlight combobox.
        /// </summary>
        private void set_choose_flight_combobox()
        {
            string[] flights = business_Logic.get_flights();

            for (int i = 0; i < flights.Length; i++)
            {
                cbChooseFlight.Items.Add(flights[i]);
            }
        }



        /// <summary>
        /// Compare the array of seats with the seat number of passengers in the passenger list
        /// If the seat is taken, make it red
        /// </summary>
        private void update_first_flight()
        {
            try
            {
                for (int i = 0; i < first_flight_seats.Length; i++)//Go through the array of seats
                {
                    first_flight_seats[i].Background = Brushes.Blue;//Make it blue.  If this seat is not taken
                    //it will stay blue.
                    for (int j = 0; j < passengers.Count(); j++)//For each seat in the array, compare it with
                                                                //the seat numbers of the passengers in the list of passengers.
                    {
                        //Check if this seat is taken
                        if (passengers[j].get_flight_id() == 1 && passengers[j].get_seat_number() == (i + 1))
                        {
                            first_flight_seats[i].Background = Brushes.Red;//If this seat is taken, make it red
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }
        /// <summary>
        /// Compare the array of seats with the seat number of passengers in the passenger list
        /// If the seat is taken, make it red
        /// </summary>
        private void update_second_flight()
        {
            try
            {
                for (int i = 0; i < second_flight_seats.Length; i++)
                {
                    second_flight_seats[i].Background = Brushes.Blue;
                    for (int j = 0; j < passengers.Count(); j++)
                    {
                        //Check if this seat is taken
                        if (passengers[j].get_flight_id() == 2 && passengers[j].get_seat_number() == (i + 1))
                        {
                            second_flight_seats[i].Background = Brushes.Red;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            Passenger passenger_to_delete = new Passenger();
            for(int i = 0; i < passengers.Count; i++)
            {//The user has selected the passenger to delete from the combobox.  Now we need to find that same passenger
                //in the list of passengers
                if (passengers[i].get_full_name() == cbChoosePassenger.SelectedItem.ToString())
                {
                    passenger_to_delete.set_first_name(passengers[i].get_first_name());
                    passenger_to_delete.set_last_name(passengers[i].get_last_name());
                    passengers.Remove(passengers[i]);
                }
            }
            //Make sure passenger_to_delete is a passenger and is not empty
            if (passenger_to_delete.get_full_name().Length > 0)//Give business_Logic the first and last names so the passenger can be deleted
            {
                business_Logic.delete_passenger(passenger_to_delete.get_first_name(), passenger_to_delete.get_last_name());
                if (cbChooseFlight.SelectedIndex == 0)
                    update_first_flight();

                else
                    update_second_flight();
            }

            //Update passenger combobox
            cbChoosePassenger.Items.Clear();
            string[] passenger_array = business_Logic.get_passengers(cbChooseFlight.SelectedIndex);
            //Would be nice if code from another class executed the SQL above, added each passenger into a Passenger object, then into a list of Passengers to be returned and bound to the combo box
            for (int i = 0; i < passenger_array.Length; i++)
            {

                cbChoosePassenger.Items.Add(passenger_array[i]);
            }

        }

        private void Seat1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (cbChoosePassenger.SelectedItem != null)
            {
                Passenger selected_passenger = new Passenger();


                int i = 0;
                while (i < passengers.Count && passengers[i].get_full_name() != cbChoosePassenger.SelectedItem.ToString())
                {
                    i++;
                }
                selected_passenger.set_first_name(passengers[i].get_first_name());
                selected_passenger.set_last_name(passengers[i].get_last_name());
                selected_passenger.set_flight_id(passengers[i].get_flight_id());
               

                //Make sure selected_passenger is a passenger and is not empty
                if (selected_passenger.get_full_name().Length > 0)//Give business_Logic the first and last names so the passenger can be deleted
                {
                    int seat_number = 0;//We need to convert the label the user clicked on into a number

                    //This will make seat_number the index of the selected seat in the array
                    while (seat_number < first_flight_seats.Length && sender != first_flight_seats[seat_number])
                    {
                        seat_number++;
                    }


                    //Make sure the above loop worked
                    if (sender == first_flight_seats[seat_number])
                    {
                        
                        seat_number++;//Have to add one to the seat_number because the array of seats starts counting at 0.

                        //This will update the seat number for this passenger in the passengers list
                        passengers[i].set_seat_number(seat_number);

                        //This will update the seat number for this passenger in the database
                        //First see if this is a new passenger.  If it is we will need to use a different SQL statement 
                        //to change their seat number, and then set the new_passenger bool to false
                        if (passengers[i].get_new_passenger())
                        {
                            business_Logic.insert_into_link_table(flights[0].get_flight_id(),
                            selected_passenger.get_first_name(), selected_passenger.get_last_name(), seat_number);
                            passengers[i].set_new_passenger(false);
                        }

                        else
                            business_Logic.update_seat(flights[0].get_flight_id(),
                                selected_passenger.get_first_name(), selected_passenger.get_last_name(), seat_number);

                        update_first_flight();
                        //Change the selected seat to green
                        first_flight_seats[(seat_number - 1)].Background = Brushes.Green;
                    }
                }
            }
        }

        private void SeatA1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (cbChoosePassenger.SelectedItem != null)
            {
                Passenger selected_passenger = new Passenger();


                int i = 0;
                while (i < passengers.Count && passengers[i].get_full_name() != cbChoosePassenger.SelectedItem.ToString())
                {
                    i++;
                }
                selected_passenger.set_first_name(passengers[i].get_first_name());
                selected_passenger.set_last_name(passengers[i].get_last_name());
                selected_passenger.set_flight_id(passengers[i].get_flight_id());
                

                //Make sure selected_passenger is a passenger and is not empty
                if (selected_passenger.get_full_name().Length > 0)//Give business_Logic the first and last names so the passenger can be deleted
                {
                    int seat_number = 0;//We need to convert the label the user clicked on into a number

                    //This will make seat_number the index of the selected seat in the array
                    while (seat_number < second_flight_seats.Length && sender != second_flight_seats[seat_number])
                    {
                        seat_number++;
                    }


                    //Make sure the above loop worked
                    if (sender == second_flight_seats[seat_number])
                    {
                        
                        seat_number++;//Have to add one to the seat_number because the array of seats starts counting at 0.

                        //This will update the seat number for this passenger in the passengers list
                        passengers[i].set_seat_number(seat_number);

                        //This will update the seat number for this passenger in the database
                        //First see if this is a new passenger.  If it is we will need to use a different SQL statement 
                        //to change their seat number, and then set the new_passenger bool to false
                        if (passengers[i].get_new_passenger())
                        {
                            business_Logic.insert_into_link_table(flights[1].get_flight_id(),
                            selected_passenger.get_first_name(), selected_passenger.get_last_name(), seat_number);
                            passengers[i].set_new_passenger(false);
                        }

                        else
                            business_Logic.update_seat(flights[1].get_flight_id(),
                                selected_passenger.get_first_name(), selected_passenger.get_last_name(), seat_number);

                        update_second_flight();
                        //Change the selected seat to green
                        second_flight_seats[(seat_number - 1)].Background = Brushes.Green;
                    }
                }
            }
        }
    }


}
