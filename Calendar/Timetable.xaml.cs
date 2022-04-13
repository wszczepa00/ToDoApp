using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Calendar
{
    /// <summary>
    /// Logika interakcji dla klasy Timetable.xaml
    /// </summary>
    public partial class Timetable : Window
    {
        public Timetable()
        {
            InitializeComponent();
            LoadMoodSP();
            var window = (MainWindow)Application.Current.MainWindow;
            window.WriteTasksToListBox(TTEventsDone, window.MyCalendar.SelectedDate.Value, true);
            window.WriteTasksToListBox(TTEventsTODO, window.MyCalendar.SelectedDate.Value, false);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TitleField.Text != "" & BegginingField.Text != "" & EndField.Text != "")
            {
                if (TimeSpan.TryParse(BegginingField.Text, out var output) &
                    TimeSpan.TryParse(EndField.Text, out var output2))
                {
                    if (TimeSpan.Parse(BegginingField.Text) <= TimeSpan.Parse(EndField.Text))
                    {

                        Event ev = new Event();
                        var window = (MainWindow)Application.Current.MainWindow;
                        window.AddNewEvent(ev, window.MyCalendar.SelectedDate.Value, TimeSpan.Parse(BegginingField.Text),
                                            TimeSpan.Parse(EndField.Text), TitleField.Text, DescriptionField.Text,
                                            Convert.ToBoolean(DoneField.IsChecked));
                        window.ReloadTasksInListBox(TTEventsTODO, window.MyCalendar.SelectedDate.Value, false);
                        window.ReloadTasksInListBox(TTEventsDone, window.MyCalendar.SelectedDate.Value, true);

                        TitleField.Text = "";
                        BegginingField.Text = "";
                        EndField.Text = "";
                        DescriptionField.Text = "";
                        DoneField.IsChecked = false;


                        window.ReloadYourWeekView(window.MyCalendar.SelectedDate.Value);



                    }
                    else
                    {
                        Error er = new Error();
                        er.ErrorWindow.Text = "Error: Beggining can't be after End.";
                        er.Show();
                    }
                }
                else
                {
                    Error er = new Error();
                    er.ErrorWindow.Text = "Error: incorrect hour format (hh:mm)";
                    er.Show();
                }
            }
            else
            {
                Error er = new Error();
                er.ErrorWindow.Text = "Error: empty Title, beggining or End.";
                er.Show();
            }
        }

        private void btnMoreInfo_Click(object sender, RoutedEventArgs e)
        {
            if (TTEventsDone.SelectedItem == null && TTEventsTODO.SelectedItem == null)
            {
                Error er = new Error();
                er.ErrorWindow.Text = "Error: No items selected.";
                er.Show();
            }
            else if ((TTEventsTODO.SelectedItems.Count + TTEventsDone.SelectedItems.Count) > 1)
            {
                Error er = new Error();
                er.ErrorWindow.Text = "Error: More than one item selected";
                er.Show();
            }
            else if (TTEventsTODO.SelectedItems.Count == 1)
            {

                using (var context = new CalendarContext())
                {
                    var query = context.Events.Where(s => ("ev" + s.Id.ToString() == ((ListBoxItem)TTEventsTODO.SelectedItem).Name)).FirstOrDefault();

                    MessageBox.Show("Date: " + query.DayId.ToString("dd.MM.yyyy") + "\nBeggining of the event: " + query.Beggining + "\nEnd of the event: " + query.End + "\nDescription: " + query.Description, query.Name);

                }
            }
            else if (TTEventsDone.SelectedItems.Count == 1)
            {

                using (var context = new CalendarContext())
                {
                    var query = context.Events.Where(s => ("ev" + s.Id.ToString() == ((ListBoxItem)TTEventsDone.SelectedItem).Name)).FirstOrDefault();

                    MessageBox.Show("Date: " + query.DayId.ToString("dd.MM.yyyy") + "\nBeggining of the event: " + query.Beggining + "\nEnd of the event: " + query.End + "\nDescription: " + query.Description, query.Name);

                }
            }
            TTEventsDone.UnselectAll();
            TTEventsTODO.UnselectAll();
        }

        private void btnDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (TTEventsDone.SelectedItem == null && TTEventsTODO.SelectedItem == null)
            {
                Error er = new Error();
                er.ErrorWindow.Text = "Error: No items selected.";
                er.Show();
            }
            else
            {
                var window = (MainWindow)Application.Current.MainWindow;

                using (var context = new CalendarContext())
                {
                    foreach (ListBoxItem selectedItem in TTEventsTODO.SelectedItems)
                    {

                        var query = context.Events.Where(s => ("ev" + s.Id.ToString() == selectedItem.Name)).FirstOrDefault();

                        window.DeleteEvent(query);

                    }
                    foreach (ListBoxItem selectedItem in TTEventsDone.SelectedItems)
                    {

                        var query = context.Events.Where(s => ("ev" + s.Id.ToString() == selectedItem.Name)).FirstOrDefault();

                        window.DeleteEvent(query);

                    }

                }
                window.ReloadTasksInListBox(TTEventsTODO, window.MyCalendar.SelectedDate.Value, false);
                window.ReloadTasksInListBox(TTEventsDone, window.MyCalendar.SelectedDate.Value, true);

                window.ReloadYourWeekView(window.MyCalendar.SelectedDate.Value);
            }
        }

        private void btnChangeStatus_Click(object sender, RoutedEventArgs e)
        {
            if (TTEventsDone.SelectedItem == null && TTEventsTODO.SelectedItem == null)
            {
                Error er = new Error();
                er.ErrorWindow.Text = "Error: No items selected.";
                er.Show();
            }
            else
            {
                var window = (MainWindow)Application.Current.MainWindow;

                using (var context = new CalendarContext())
                {
                    foreach (ListBoxItem selectedItem in TTEventsTODO.SelectedItems)
                    {

                        var query = context.Events.Where(s => ("ev" + s.Id.ToString() == selectedItem.Name)).FirstOrDefault();

                        window.ChangeEventStatus(query);

                    }
                    foreach (ListBoxItem selectedItem in TTEventsDone.SelectedItems)
                    {

                        var query = context.Events.Where(s => ("ev" + s.Id.ToString() == selectedItem.Name)).FirstOrDefault();

                        window.ChangeEventStatus(query);

                    }

                }
                window.ReloadTasksInListBox(TTEventsTODO, window.MyCalendar.SelectedDate.Value, false);
                window.ReloadTasksInListBox(TTEventsDone, window.MyCalendar.SelectedDate.Value, true);

                window.ReloadYourWeekView(window.MyCalendar.SelectedDate.Value);
            }


        }
        private void LoadMoodSP()
        {
            var window = (MainWindow)Application.Current.MainWindow;
            using (var context = new CalendarContext())
            {
                var query = context.Dates.Where(s => s.Id == window.MyCalendar.SelectedDate.Value).FirstOrDefault();
                if (query.Mood != 0)
                {

                    TBRate.Text = " Your mood in this day: " + query.Mood;
                    ButtonRate.Content = "Change your rate";
                }

            }

        }

        private void ButtonRate_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                var window = (MainWindow)Application.Current.MainWindow;
                int inputRate = Int32.Parse(TBoxRate.Text);

                if (inputRate <= 10 && inputRate >= 1)
                {
                    using (var context = new CalendarContext())
                    {
                        var query = context.Dates.Where(s => s.Id == window.MyCalendar.SelectedDate.Value).FirstOrDefault();
                        window.ChangeMoodRate(query, inputRate);
                        LoadMoodSP();
                        window.ReloadYourWeekView(window.MyCalendar.SelectedDate.Value);
                    }
                }
                else
                {
                    Error er = new Error();
                    er.ErrorWindow.Text = "Error: Rating out of range(1-10)";
                    er.Show();


                }
            }
            catch (Exception myException)
            {
                MessageBox.Show(myException.Message + "It should be integer between 1-10 ");
            }
            TBoxRate.Text = "";
        }

        private void ExpAddEvent_Expanded(object sender, RoutedEventArgs e)
        {
            ((Expander)sender).BringIntoView();
        }

    }

}