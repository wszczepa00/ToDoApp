using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
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
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public TextBlock tbDay = new TextBlock();
        public CheckBox checkBox = new CheckBox();
        public DateTime day1;
        public DateTime day2;
        public DateTime day3;
        public DateTime day4;
        public DateTime day5;

        public MainWindow()
        {
            InitializeComponent();
            ApiHelper.InitializeClient();

            YourWeekView_Initialized();
        }

        private void DecadeButton_Click(object sender, RoutedEventArgs e)
        {
            MyCalendar.DisplayMode = CalendarMode.Decade;
        }

        private void YearButton_Click(object sender, RoutedEventArgs e)
        {
            MyCalendar.DisplayMode = CalendarMode.Year;
        }

        private void MonthButton_Click(object sender, RoutedEventArgs e)
        {
            MyCalendar.DisplayMode = CalendarMode.Month;
        }

        private void ColorChange_Click(object sender, RoutedEventArgs e)
        {
            if (MainStackPanel.Background == Brushes.AliceBlue)
            {
                ColorChange.Content = "Light mode";
                MainStackPanel.Background = Brushes.Black;
                ColorChange.Background = Brushes.Black;
                ColorChange.Foreground = Brushes.WhiteSmoke;
                ColorChange.BorderBrush = Brushes.WhiteSmoke;
                DecadeButton.Background = Brushes.Black;
                DecadeButton.Foreground = Brushes.WhiteSmoke;
                DecadeButton.BorderBrush = Brushes.WhiteSmoke;
                MonthButton.Background = Brushes.Black;
                MonthButton.Foreground = Brushes.WhiteSmoke;
                MonthButton.BorderBrush = Brushes.WhiteSmoke;
                YearButton.Background = Brushes.Black;
                YearButton.Foreground = Brushes.WhiteSmoke;
                YearButton.BorderBrush = Brushes.WhiteSmoke;
                MyCalendar.Background = Brushes.Black;
                MyCalendar.Foreground = Brushes.WhiteSmoke;
                MyCalendar.BorderBrush = Brushes.Black;
                MyScrollViewer.Background = Brushes.Black;
                MyScrollViewer.Foreground = Brushes.WhiteSmoke;
                Border1.Background = Brushes.Black;
                Border2.Background = Brushes.Black;
                Border3.Background = Brushes.Black;
                Border4.Background = Brushes.Black;
                Border5.Background = Brushes.Black;
                Border2.BorderBrush = Brushes.DarkGray;
                tbDayC2.Foreground = Brushes.DarkGray;
                /* Border6.Background = Brushes.Black;
                 Border7.Background = Brushes.Black;*/
            }
            else if (MainStackPanel.Background == Brushes.Black)
            {
                ColorChange.Content = "Dark mode";
                MainStackPanel.Background = Brushes.AliceBlue;
                ColorChange.Background = Brushes.LightBlue;
                ColorChange.Foreground = Brushes.Black;
                ColorChange.BorderBrush = Brushes.Black;
                DecadeButton.Background = Brushes.LightBlue;
                DecadeButton.Foreground = Brushes.Black;
                DecadeButton.BorderBrush = Brushes.Black;
                MonthButton.Background = Brushes.LightBlue;
                MonthButton.Foreground = Brushes.Black;
                MonthButton.BorderBrush = Brushes.Black;
                YearButton.Background = Brushes.LightBlue;
                YearButton.Foreground = Brushes.Black;
                YearButton.BorderBrush = Brushes.Black;
                MyCalendar.Background = Brushes.LightBlue;
                MyCalendar.Foreground = Brushes.Black;
                MyCalendar.BorderBrush = Brushes.LightBlue;
                MyScrollViewer.Background = Brushes.AliceBlue;
                MyScrollViewer.Foreground = Brushes.Black;
                Border1.Background = Brushes.LightBlue;
                Border2.Background = Brushes.LightBlue;
                Border3.Background = Brushes.LightBlue;
                Border4.Background = Brushes.LightBlue;
                Border5.Background = Brushes.LightBlue;
                Border2.BorderBrush = Brushes.Blue;
                tbDayC2.Foreground = Brushes.Blue;
                /*Border6.Background = Brushes.LightBlue;
                Border7.Background = Brushes.LightBlue;*/
            }
        }

        public void AddNewDay(Day day, DateTime date)
        {
            using (var context = new CalendarContext())
            {
                day.Id = date;
                context.Dates.Add(day);
                context.SaveChanges();
            }
        }

        public void AddNewEvent(Event ev, DateTime date, TimeSpan beg, TimeSpan end, string name, string desription = "", bool done = false)
        {
            using (var context = new CalendarContext())
            {

                var day = context.Dates.Where(s => s.Id == date).FirstOrDefault();


                ev.Beggining = beg;
                ev.End = end;
                ev.Name = name;
                ev.Description = desription;
                ev.Done = done;
                ev.DayId = day.Id;
                //ev.DayId = date;
                context.Events.Add(ev);
                context.SaveChanges();
            }
        }

        public void DeleteEvent(Event ev)
        {
            using (var context = new CalendarContext())
            {
                var temp = context.Events.Single(s => s.Id == ev.Id);
                context.Events.Remove(temp);
                context.SaveChanges();

            }

        }
        public void ChangeEventStatus(Event ev)
        {
            using (var context = new CalendarContext())
            {
                var temp = context.Events.Single(s => s.Id == ev.Id);
                //bool status = temp.Done;
                context.Entry(temp).Entity.Done = !temp.Done;
                context.SaveChanges();

            }

        }

        public void ChangeMoodRate(Day day, int mood)
        {
            using (var context = new CalendarContext())
            {
                var temp = context.Dates.Single(s => s.Id == day.Id);
                context.Entry(temp).Entity.Mood = mood;
                context.SaveChanges();

            }

        }

        //private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    Timetable TT = new Timetable();
        //    TT.Show();
        //    TT.Title = MyCalendar.SelectedDate.Value.Date.ToShortDateString();

        //}

        private void MyCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            using (var context = new CalendarContext())
            {
                Day day = new Day
                {
                    Id = MyCalendar.SelectedDate.Value
                };

                if (!context.Dates.Any(d => d.Id == day.Id))
                {
                    AddNewDay(day, day.Id);
                }

            }
            Timetable TT = new Timetable();
            TT.Owner = this;  // new window on top
            TT.ResizeMode = ResizeMode.CanResize;

            TT.Show();

            TT.Title = MyCalendar.SelectedDate.Value.Date.ToShortDateString();

        }

        public void WriteTasksToListBox(ListBox listBox, DateTime date, bool isDone)
        {
            using (var context = new CalendarContext())
            {
                IQueryable query;
                if (isDone == true)
                {
                    query = context.Events.Where(s => (s.DayId == date) && (s.Done == true));
                }
                else
                {
                    query = context.Events.Where(s => (s.DayId == date) && (s.Done == false));
                }
                // var query = myTaskContext.Tasks;
                foreach (Event ev in query)
                {
                    ListBoxItem item = new ListBoxItem
                    {
                        Content = ev.Name,
                        Name = "ev" + ev.Id
                    };
                    if (isDone == true)
                    {
                        item.Foreground = Brushes.Green;
                    }
                    else
                    {
                        item.Foreground = Brushes.Red;
                    }
                    listBox.Items.Add(item);

                }


            }
        }



        public void WriteTasksToStackPanel(StackPanel stackPanel, DateTime date, bool done)
        {
            using (var context = new CalendarContext())
            {
                var query = context.Events.Where(s => (s.DayId == date) && (s.Done == done));
                foreach (Event ev in query)
                {
                    TextBlock temp = new TextBlock
                    {
                        Text = ev.Name,

                        //Background = Brushes.Red
                    };

                    stackPanel.Children.Add(temp);
                }
            }


        }

        public void ReloadTasksInListBox(ListBox listBox, DateTime date, bool isDone)
        {
            while (listBox.Items.Count > 0)
            {
                listBox.Items.RemoveAt(listBox.Items.Count - 1);
            }

            WriteTasksToListBox(listBox, date, isDone);
        }

        public void ReloadStackPanel(StackPanel stackPanel, DateTime date, bool done)
        {
            using (var context = new CalendarContext())
            {
                {
                    while (stackPanel.Children.Count > 0)
                    {
                        stackPanel.Children.RemoveAt(stackPanel.Children.Count - 1);
                    }
                    WriteTasksToStackPanel(stackPanel, date, done);

                }
            }
        }

        private void YourWeekView_Initialized()
        {
            day1 = DateTime.Today.AddDays(-1);
            day2 = DateTime.Today;
            day3 = DateTime.Today.AddDays(1);
            day4 = DateTime.Today.AddDays(2);
            day5 = DateTime.Today.AddDays(3);

            tbDayC1.Text = "Yesterday";
            tbDayC2.Text = "Today";
            tbDayC3.Text = "Tomorrow";
            tbDayC4.Text = day4.Date.ToString("dd/MM/yyyy");
            tbDayC5.Text = day5.Date.ToString("dd/MM/yyyy");

            WriteTasksToStackPanel(spB1, day1, true);
            WriteTasksToStackPanel(spB2, day2, false);
            WriteTasksToStackPanel(spB3, day3, false);
            WriteTasksToStackPanel(spB4, day4, false);
            WriteTasksToStackPanel(spB5, day5, false);

            LoadMoodWeekView(B1TBRate, day1);
            LoadMoodWeekView(B2TBRate, day2);
            LoadMoodWeekView(B3TBRate, day3);
            LoadMoodWeekView(B4TBRate, day4);
            LoadMoodWeekView(B5TBRate, day5);




        }

        public string City { get; set; } = "Wroclaw";
        private async Task LoadTemperature(string City)
        {

            var weather = await WeatherProcessor.LoadWeather(City);

            WeatherCity.Text = City;
            ImageSource imageSource = new ImageSourceConverter().ConvertFromString($"https://openweathermap.org/img/w/{weather.weather[0].icon}.png") as ImageSource;
            WeatherIcon.Source = imageSource;
            WeatherTemperature.Text = (Math.Round((weather.main.temp - 273.15) * 2, MidpointRounding.AwayFromZero) / 2).ToString() + "°C " + weather.weather[0].main;
            WeatherSunrise.Text = "Sunrise: " + convertDateTime(weather.sys.sunrise).ToShortTimeString();
            WeatherSunset.Text = "Sunset: " + convertDateTime(weather.sys.sunset).ToShortTimeString();
            WeatherPressure.Text = "Pressure: " + weather.main.pressure.ToString() + " hPa";
        }

        DateTime convertDateTime(long millisec)
        {
            DateTime day = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc).ToLocalTime();
            day = day.AddSeconds(millisec).ToLocalTime();

            return day;
        }

        private void LoadMoodWeekView(TextBlock textBlock, DateTime dateTime)
        {

            using (var context = new CalendarContext())
            {

                var query = context.Dates.Where(s => s.Id == dateTime).FirstOrDefault();

                if (query != null)
                {
                    if (query.Mood != 0)
                    {

                        textBlock.Text = " Your mood in this day: " + query.Mood;

                    }
                    else
                    {
                        textBlock.Text = "Not rate your mood yet";
                    }

                }
                else
                {
                    textBlock.Text = "Not rate your mood yet";
                }
            }
        }

        public void ReloadYourWeekView(DateTime date)
        {

            if (MyCalendar.SelectedDate.Value >= day1 && MyCalendar.SelectedDate.Value <= day5)
            {

                if (MyCalendar.SelectedDate.Value == day1)
                {
                    ReloadStackPanel(spB1, day1, true);
                    LoadMoodWeekView(B1TBRate, day1);
                }
                else if (MyCalendar.SelectedDate.Value == day2)
                {
                    ReloadStackPanel(spB2, day2, false);
                    LoadMoodWeekView(B2TBRate, day2);
                }
                else if (MyCalendar.SelectedDate.Value == day3)
                {
                    ReloadStackPanel(spB3, day3, false);
                    LoadMoodWeekView(B3TBRate, day3);
                }
                else if (MyCalendar.SelectedDate.Value == day4)
                {
                    ReloadStackPanel(spB4, day4, false);
                    LoadMoodWeekView(B4TBRate, day4);
                }
                else
                {
                    ReloadStackPanel(spB5, day5, false);
                    LoadMoodWeekView(B5TBRate, day5);
                }

            }





        }
        private void Window_Initialized(object sender, EventArgs e)
        {
            ////if (CDateTime.Today.ToString("dd/MM/yyyy"))
            //using (var context = new CalendarContext())
            //{
            //    Day day = new Day();
            //    day.Id = DateTime.Now;
            //    context.Dates.Add(day);
            //context.SaveChanges();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(ApiKeys.ApiKey != "")
            {
                try
                { 
                    await LoadTemperature(City);
                }
                catch (Exception myException)
                {
                    MessageBox.Show(myException.Message + "  Incorrect Api Key");
                }
            }
            else
            {
                WeatherTemperature.Text = "You have to pass your API Key in APIKeysLocal.cs file";
            }
        }

        private async void btnChangeCity_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadTemperature(WeatherCity.Text);
            }

            catch (Exception myException)
            {
                MessageBox.Show(myException.Message + "  Incorrect name of City");

            }
        }

        //}

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    Style MyCalendarStyle = new Style(typeof(Calendar));
        //    MyCalendarStyle.Setters.Add(new Setter(Calendar.BackgroundProperty, Brushes.LightBlue));
        //    MyCalendarStyle.Setters.Add(new Setter(Calendar.ForegroundProperty, Brushes.Black));
        //    MyCalendarStyle.Setters.Add(new Setter(Calendar.BorderBrushProperty, Brushes.LightBlue));
        //    Resources.Add(typeof(Calendar), MyCalendarStyle);

        //    Style ButtonStyle = new Style(typeof(Button));
        //    ButtonStyle.Setters.Add(new Setter(Button.BackgroundProperty, Brushes.LightBlue));
        //    ButtonStyle.Setters.Add(new Setter(Button.ForegroundProperty, Brushes.Black));
        //    Resources.Add(typeof(Button), ButtonStyle);
        //}
    }
}
