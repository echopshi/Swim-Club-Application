using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SwimLib;
using System.IO;

namespace SwimForm
{
    public partial class Form1 : Form
    {
        private const string DELETE_MESSAGE = "Are you sure to delete this item?";
        private const string DUPLICATE_MESSAGE = "This information already exists, please do not add duplicate value!";
        private const string MODIFY_MESSAGE = "Are you sure you want to modify this item? If you click OK then the older one will be delete, and please click Add to confirm modification";
        private const string MODIFY = "Modify Item";
        private const string DELETE = "Delete Item";
        private const string ERROR = "Error";

        List<SwimMeet> swimMeets = new List<SwimMeet>();
        List<Registrant> swimmers = new List<Registrant>();
        List<Swimmer> newSwimmers = new List<Swimmer>();
        List<Coach> coachs = new List<Coach>();
        List<Club> clubs = new List<Club>();
        List<Event> events = new List<Event>();

        public Form1()
        {
            InitializeComponent();
            createDataInfo();
        }

        void createDataInfo() {
            loadSwimmerMeet();
            loadRegistrant();
            loadSwimmers();
            loadClubs();
            loadEvents();

            SwimData.DataSource = swimMeets;
            SwimData.MultiSelect = false;
            SwimData.ReadOnly = true;
            foreach(SwimMeet sm in swimMeets) {
                SwimMeetList.Items.Add(sm.Name);
            }

            SwimmerData.DataSource = swimmers;
            SwimmerData.MultiSelect = false;
            SwimmerData.ReadOnly = true;
            foreach (Coach coach in coachs)
            {
                AssignCoachesList.Items.Add(coach.Name);
                AddSwimmerToClub.Items.Add(coach.Name);
                SwimmerList.Items.Add(coach.Name);
            }
            foreach (Swimmer swimmer in newSwimmers)
            {
                AssignSwimmerList.Items.Add(swimmer.Name);
                AddSwimmerToClub.Items.Add(swimmer.Name);
                SwimmerList.Items.Add(swimmer.Name);
            }

            ClubData.DataSource = clubs;
            ClubData.MultiSelect = false;
            ClubData.ReadOnly = true;
            foreach (Club clb in clubs) {
                ClubList.Items.Add(clb.Name);
            }

            for(int i = 0; i<events.Count; i++) {
                EventList.Items.Add(events[i] + " " + i.ToString());
            }

            listDistance.DataSource = new List<EventDistance>() { EventDistance._50, EventDistance._100, EventDistance._200, EventDistance._400, EventDistance._800, EventDistance._1500};
            listStroke.DataSource = new List<Stroke>() { Stroke.Backstroke, Stroke.Breaststroke, Stroke.Butterfly, Stroke.Freestyle, Stroke.IndividualMedley};
        }

        void loadRegistrant() {
            SwimmersManager smg = new SwimmersManager();
            smg.Swimmers = swimmers;
            smg.Load("RegistrantOut.txt", "|");
            swimmers =  smg.Swimmers;
        }

        void loadSwimmers() {
            foreach (Registrant re in swimmers)
            {
                newSwimmers.Add(new Swimmer(re.Name, re.DateOfBirth, re.Address, re.PhoneNumber));
            }
        }

        void loadClubs() {
            ClubsManager clg = new ClubsManager();
            clg.Clubs = clubs;
            clg.Load("ClubsOut.txt", "|");
            clubs = clg.Clubs;
        }

        void loadSwimmerMeet() {
            char deli = '|';
            string record;
            FileStream fileIn = null;
            StreamReader reader = null;
            try
            {
                fileIn = new FileStream("SwimMeetsOut.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fileIn);

                record = reader.ReadLine();
                while (record != null)
                {
                    string[] fields = record.Split(deli);
                    SwimMeet sm = new SwimMeet();
                    sm.Name = fields[0];
                    sm.StartDate = Convert.ToDateTime(fields[1]);
                    sm.EndDate = Convert.ToDateTime(fields[2]);
                    sm.NoOfLanes = Convert.ToInt32(fields[4]);
                    if (fields[3] == "SCM")
                    {
                        sm.MyCourse = PoolType.SCM;
                    }
                    else if (fields[3] == "LCM") {
                        sm.MyCourse = PoolType.LCM;
                    }
                    else
                    {
                        sm.MyCourse = PoolType.SCY;
                    }

                    swimMeets.Add(sm);
                    record = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fileIn != null)
                {
                    fileIn.Close();
                }
            }
        }

        void loadEvents()
        {
            char deli = '|';
            string record;
            FileStream fileIn = null;
            StreamReader reader = null;

            try
            {
                fileIn = new FileStream("EventsOut.txt", FileMode.Open, FileAccess.Read);
                reader = new StreamReader(fileIn);

                record = reader.ReadLine();
                while (record != null)
                {
                    string[] fields = record.Split(deli);
                    Event myEvent = new Event();
                    if (fields[0] == "_50")
                    {
                        myEvent.Distance = EventDistance._50;
                    }
                    else if (fields[0] == "_100")
                    {
                        myEvent.Distance = EventDistance._100;
                    }
                    else if (fields[0] == "_200")
                    {
                        myEvent.Distance = EventDistance._200;
                    }
                    else if (fields[0] == "_400")
                    {
                        myEvent.Distance = EventDistance._400;
                    }
                    else if (fields[0] == "_800")
                    {
                        myEvent.Distance = EventDistance._800;
                    }
                    else if (fields[0] == "_1500")
                    {
                        myEvent.Distance = EventDistance._1500;
                    }

                    if (fields[1] == "Freestyle")
                    {
                        myEvent.Stroke = Stroke.Freestyle;
                    }
                    else if (fields[1] == "Butterfly")
                    {
                        myEvent.Stroke = Stroke.Butterfly;
                    }
                    else if (fields[1] == "Backstroke")
                    {
                        myEvent.Stroke = Stroke.Backstroke;
                    }
                    else if (fields[1] == "Breaststroke")
                    {
                        myEvent.Stroke = Stroke.Breaststroke;
                    }
                    else if (fields[1] == "IndividualMedley")
                    {
                        myEvent.Stroke = Stroke.IndividualMedley;
                    }

                    events.Add(myEvent);
                    record = reader.ReadLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (reader != null)
                {
                    reader.Close();
                }
                if (fileIn != null)
                {
                    fileIn.Close();
                }
            }
        }
        void saveSwimmers() {
            SwimmersManager smg = new SwimmersManager();
            smg.Swimmers = swimmers;
            smg.Save("RegistrantOut.txt","|");
        }

        void saveClubs() {
            ClubsManager clg = new ClubsManager();
            clg.Clubs = clubs;
            clg.Save("ClubsOut.txt","|");
        }

        void saveSwimMeets() {
            FileStream fileOut = null;
            StreamWriter writer = null;
            string delimeter = "|";
            try
            {
                fileOut = new FileStream("SwimMeetsOut.txt", FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(fileOut);

                foreach (SwimMeet sm in swimMeets)
                {
                    writer.WriteLine(sm.Name + delimeter + sm.StartDate + delimeter + sm.EndDate
                        + delimeter + sm.MyCourse + delimeter + sm.NoOfLanes);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (fileOut != null)
                {
                    fileOut.Close();
                }
            }
        }

        void saveEvents()
        {
            FileStream fileOut = null;
            StreamWriter writer = null;
            string delimeter = "|";
            try
            {
                fileOut = new FileStream("EventsOut.txt", FileMode.Create, FileAccess.Write);
                writer = new StreamWriter(fileOut);

                foreach (Event ev in events)
                {
                    writer.WriteLine(ev.Distance + delimeter + ev.Stroke);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                if (writer != null)
                {
                    writer.Close();
                }
                if (fileOut != null)
                {
                    fileOut.Close();
                }
            }
        }

        string convertDateTime(string input) {
            input = input.Replace("/", "");
            if (input.Length == 6)
            {
                input = "0" + input.Substring(0, 1) + "0" + input.Substring(1, 5);
            }
            else if (input.Length == 7)
            {
                input = "0" + input;
            }

            return input;
        }

        void refreshView()
        {
            SwimData.DataSource = null;
            SwimmerData.DataSource = null;
            ClubData.DataSource = null;

            swimmers.Clear();
            foreach (Swimmer swimmer in newSwimmers)
            {
                swimmers.Add(swimmer);
            }
            foreach (Coach coach in coachs) {
                swimmers.Add(coach);
            }

            SwimData.DataSource = swimMeets;
            SwimmerData.DataSource = swimmers;
            ClubData.DataSource = clubs;
        }

        private void CCreate_Click(object sender, EventArgs e)
        {
            Address address = new Address(ClubStreet.Text, ClubCity.Text, ClubPro.Text, ClubPostal.Text);
            Club club = new Club();
            club.Name = ClubName.Text;
            string phone = ClubPhone.Text.Replace("-", string.Empty).Replace("(", string.Empty).Replace(") ", string.Empty);
            club.PhoneNumber = Convert.ToInt64(phone);
            club.Address = address;

            if (!clubs.Contains(club))
            {
                clubs.Add(club);
                ClubList.Items.Add(club.Name);
            }
            else
            {
                MessageBox.Show(DUPLICATE_MESSAGE, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            saveClubs();
            refreshView();
        }

        private void CClear_Click(object sender, EventArgs e)
        {
            ClubStreet.Text = "";
            ClubCity.Text = "";
            ClubPro.Text = "";
            ClubPostal.Text = "";
            ClubName.Text = "";
            ClubPhone.Text = "";
        }

        private void CAddSwimmer_Click(object sender, EventArgs e)
        {
            string clubName = ClubList.SelectedItem.ToString();
            string swimmerName = AddSwimmerToClub.SelectedItem.ToString();

            foreach (Club club in clubs) {
                if (club.Name == clubName) {
                    foreach (Coach swimmer in coachs) {
                        if (swimmer.Name == swimmerName) {
                            try
                            {
                                club.AddCoach(swimmer);
                            }
                            catch (Exception ex) {
                                MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        }
                    }
                    foreach (Swimmer swimmer in newSwimmers)
                    {
                        if (swimmer.Name == swimmerName)
                        {
                            try
                            {
                                club.AddSwimmer(swimmer);
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            break;
                        }
                    }

                }
            }

            refreshView();
        }

        private void SCreate_Click(object sender, EventArgs e)
        {
            Address address = new Address(RegStreet.Text, RegCity.Text, RegPro.Text, RegPostal.Text);
            if (IfCoach.Checked)
            {
                Coach swimmer = new Coach();
                swimmer.Credentials = CoachCre.Text;
                swimmer.Name = RegName.Text;
                string phone = RegPhone.Text.Replace("-", string.Empty).Replace("(", string.Empty).Replace(") ", string.Empty);
                swimmer.PhoneNumber = Convert.ToInt64(phone);
                swimmer.Address = address;
                try
                {
                    swimmer.DateOfBirth = Convert.ToDateTime(dateofBirth.Text);
                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK,MessageBoxIcon.Error);
                }

                if (!coachs.Contains(swimmer))
                {
                    coachs.Add(swimmer);
                    AssignCoachesList.Items.Add(swimmer.Name);
                    AddSwimmerToClub.Items.Add(swimmer.Name);
                    SwimmerList.Items.Add(swimmer.Name);
                }
                else
                {
                    MessageBox.Show(DUPLICATE_MESSAGE, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (!swimmers.Contains(swimmer))
                {
                    swimmers.Add(swimmer);
                }
                else
                {
                    MessageBox.Show(DUPLICATE_MESSAGE, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                Swimmer swimmer = new Swimmer();
                swimmer.Name = RegName.Text;
                string phone = RegPhone.Text.Replace("-", string.Empty).Replace("(", string.Empty).Replace(") ", string.Empty);
                swimmer.PhoneNumber = Convert.ToInt64(phone);
                swimmer.Address = address;

                try
                {
                    swimmer.DateOfBirth = Convert.ToDateTime(dateofBirth.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (!newSwimmers.Contains(swimmer))
                {
                    newSwimmers.Add(swimmer);
                    AssignSwimmerList.Items.Add(swimmer.Name);
                    AddSwimmerToClub.Items.Add(swimmer.Name);
                    SwimmerList.Items.Add(swimmer.Name);
                }
                else
                {
                    MessageBox.Show(DUPLICATE_MESSAGE, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (!swimmers.Contains(swimmer))
                {
                    swimmers.Add(swimmer);
                }
                else
                {
                    MessageBox.Show(DUPLICATE_MESSAGE, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            refreshView();
            saveSwimmers();
        }

        private void SClear_Click(object sender, EventArgs e)
        {
            RegStreet.Text = "";
            RegCity.Text = "";
            RegPro.Text = "";
            RegPostal.Text = "";
            RegName.Text = "";
            RegPhone.Text = "";
            dateofBirth.Text = "";

        }

        private void SAssignCoach_Click(object sender, EventArgs e)
        {
            string coachName = AssignCoachesList.SelectedItem.ToString();
            string swimmerName = AssignSwimmerList.SelectedItem.ToString();

            foreach (Coach coach in coachs) {
                if (coach.Name == coachName) {
                    foreach (Swimmer swimmer in newSwimmers) {
                        if (swimmer.Name == swimmerName) {
                            try
                            {
                                coach.AddSwimmer(swimmer);
                            }
                            catch (Exception ex) {
                                MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }

            refreshView();
        }

        private void SwimCreate_Click(object sender, EventArgs e)
        {
            PoolType poolType;
            if (SCM.Checked)
            {
                poolType = PoolType.SCM;
            }
            else if (SCY.Checked)
            {
                poolType = PoolType.SCY;
            }
            else {
                poolType = PoolType.LCM;
            }

            DateTime start = new DateTime();
            try
            {
                start = Convert.ToDateTime(startDate.Text);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            DateTime end = new DateTime();
            try
            {
                end = Convert.ToDateTime(endDate.Text);
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            SwimMeet swimMeet = new SwimMeet(SMName.Text, start, end, poolType, Convert.ToInt32(nOfLanes.Text));
            
            if (!swimMeets.Contains(swimMeet))
            {
                swimMeets.Add(swimMeet);
                SwimMeetList.Items.Add(swimMeet.Name);
            }
            else
            {
                MessageBox.Show(DUPLICATE_MESSAGE, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            saveSwimMeets();
            refreshView();
        }

        private void SwimClear_Click(object sender, EventArgs e)
        {
            SCM.Checked = false;
            SCY.Checked = false;
            LCM.Checked = false;
            SMName.Text = "";
            endDate.Text = "";
            startDate.Text = "";
            nOfLanes.Text = "";
        }

        private void EventCreate_Click(object sender, EventArgs e)
        {
            Event ev = new Event();
            ev.Distance = (EventDistance)listDistance.SelectedIndex;
            ev.Stroke = (Stroke)listStroke.SelectedIndex;

            events.Add(ev);
            saveEvents();
            EventList.Items.Add(ev.ToString()+events.Count);

            refreshView();
        }

        private void EventClear_Click(object sender, EventArgs e)
        {
            listDistance.ClearSelected();
            listStroke.ClearSelected();
        }

        private void SMAddEvent_Click(object sender, EventArgs e)
        {
            string swimName = SwimMeetList.SelectedItem.ToString();
            string eventName = EventList.SelectedItem.ToString();
            int eventIndex = Convert.ToInt32(eventName.Substring(eventName.Length -1));
            
            foreach (SwimMeet sm in swimMeets) {
                if (sm.Name == swimName && !sm.Events.Contains(events[eventIndex])) {
                    sm.AddEvent(events[eventIndex]);
                }
            }

            refreshView();
        }

        private void EAddSwimmer_Click(object sender, EventArgs e)
        {
            string eventName = EventList.SelectedItem.ToString();
            int eventIndex = Convert.ToInt32(eventName.Substring(eventName.Length - 1));
            string swimmerName = SwimmerList.SelectedItem.ToString();

            foreach (Registrant swimmer in swimmers) {
                if (swimmer.Name == swimmerName) {
                    try
                    {
                        events[eventIndex].AddSwimmer(swimmer);
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            try
            {
                string swimName = SwimMeetList.SelectedItem.ToString();
                foreach (SwimMeet sm in swimMeets)
                {
                    if (sm.Name == swimName && sm.Events.Contains(events[eventIndex]))
                    {
                        sm.Seed();
                        break;
                    }
                    else if (sm.Name == swimName && !sm.Events.Contains(events[eventIndex]))
                    {
                        throw new Exception("Event is not included in Swim Meet, Please check!");
                    }
                }
            }
            catch (Exception ev)
            {
                MessageBox.Show(ev.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            refreshView();
        }

        private void EAddTime_Click(object sender, EventArgs e)
        {
            string timeInput = TimeSpanInput.Text;
            string eventName = EventList.SelectedItem.ToString();
            int eventIndex = Convert.ToInt32(eventName.Substring(eventName.Length - 1));
            string swimmerName = SwimmerList.SelectedItem.ToString();

            foreach (Registrant swimmer in swimmers) {
                if (swimmer.Name == swimmerName) {
                    try
                    {
                        events[eventIndex].EnterSwimmersTime(swimmer, timeInput);
                    }
                    catch (Exception ex) {
                        MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

            refreshView();
        }

        private void SMDelete_Click(object sender, EventArgs e)
        {
            int index = SwimData.SelectedCells[0].RowIndex;
            var message = MessageBox.Show(DELETE_MESSAGE, DELETE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                SwimMeetList.Items.Remove(swimMeets[index].Name);
                swimMeets.Remove(swimMeets[index]);
                saveSwimMeets();
                refreshView();
            }
        }

        private void SMModify_Click(object sender, EventArgs e)
        {
            int index = SwimData.SelectedCells[0].RowIndex;
            SwimMeet swim = swimMeets[index];

            MainTab.SelectedTab = SwimMeetEvent;

            SMName.Text = swim.Name;
            startDate.Text = convertDateTime(swim.StartDate.ToShortDateString());
            
            endDate.Text = convertDateTime(swim.EndDate.ToShortDateString());
            nOfLanes.Text = swim.NoOfLanes +"";
            
            if (swim.MyCourse == PoolType.SCM)
            {
                SCM.Checked = true;
            }
            else if (swim.MyCourse == PoolType.SCY)
            {
                SCY.Checked = true;
            }
            else
            {
                LCM.Checked = true;
            }

            var message = MessageBox.Show(MODIFY_MESSAGE, MODIFY, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                SwimMeetList.Items.Remove(swimMeets[index].Name);
                swimMeets.Remove(swimMeets[index]);
                saveSwimMeets();
                refreshView();
            }
        }

        private void SDelete_Click(object sender, EventArgs e)
        {
            int index = SwimmerData.SelectedCells[0].RowIndex;
            var message = MessageBox.Show(DELETE_MESSAGE, DELETE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                AddSwimmerToClub.Items.Remove(swimmers[index].Name);
                SwimmerList.Items.Remove(swimmers[index].Name);
                if (AssignSwimmerList.Items.Contains(swimmers[index].Name))
                {
                    AssignSwimmerList.Items.Remove(swimmers[index].Name);
                }
                if (AssignCoachesList.Items.Contains(swimmers[index].Name))
                {
                    AssignCoachesList.Items.Remove(swimmers[index].Name);
                }
                
                foreach (Coach coach in coachs) {
                    if (coach.Name == swimmers[index].Name) {
                        coachs.Remove(coach);
                        break;
                    }
                }
                foreach (Swimmer sw in newSwimmers) {
                    if (sw.Name == swimmers[index].Name) {
                        newSwimmers.Remove(sw);
                        break;
                    }
                }

                swimmers.Remove(swimmers[index]);
                saveSwimmers();
                refreshView();

                
            }
        }

        private void SModify_Click(object sender, EventArgs e)
        {
            int index = SwimmerData.SelectedCells[0].RowIndex;
            Registrant swimmer = swimmers[index];

            MainTab.SelectedTab = SwimmerPage;

            RegStreet.Text = swimmer.Address.Street;
            RegCity.Text = swimmer.Address.City;
            RegPro.Text = swimmer.Address.Province;
            RegPostal.Text = swimmer.Address.PostalCode;

            RegName.Text = swimmer.Name;
            RegPhone.Text = swimmer.PhoneNumber+"";
            dateofBirth.Text = convertDateTime(swimmer.DateOfBirth.ToShortDateString());

            var message = MessageBox.Show(MODIFY_MESSAGE, MODIFY, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                AddSwimmerToClub.Items.Remove(swimmers[index].Name);
                SwimmerList.Items.Remove(swimmers[index].Name);
                if (AssignSwimmerList.Items.Contains(swimmers[index].Name))
                {
                    AssignSwimmerList.Items.Remove(swimmers[index].Name);
                }
                if (AssignCoachesList.Items.Contains(swimmers[index].Name))
                {
                    AssignCoachesList.Items.Remove(swimmers[index].Name);
                }
                swimmers.Remove(swimmers[index]);
                saveSwimmers();
                refreshView();
            }
        }

        private void CDelete_Click(object sender, EventArgs e)
        {
            int index = ClubData.SelectedCells[0].RowIndex;
            var message = MessageBox.Show(DELETE_MESSAGE, DELETE, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes)
            {
                ClubList.Items.Remove(clubs[index].Name);
                clubs.Remove(clubs[index]);
                saveClubs();
                refreshView();
            }
        }

        private void CModify_Click(object sender, EventArgs e)
        {
            int index = ClubData.SelectedCells[0].RowIndex;
            Club club = clubs[index];

            MainTab.SelectedTab = ClubCreate;

            ClubStreet.Text = club.Address.Street;
            ClubCity.Text = club.Address.City;
            ClubPro.Text = club.Address.Province;
            ClubPostal.Text = club.Address.PostalCode;
            ClubName.Text = club.Name;
            ClubPhone.Text = club.PhoneNumber + "";

            var message = MessageBox.Show(MODIFY_MESSAGE, MODIFY, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (message == DialogResult.Yes) {
                ClubList.Items.Remove(clubs[index].Name);
                clubs.Remove(clubs[index]);
                saveClubs();
                refreshView();
            }
        }

        private void ShowDetailSM_Click(object sender, EventArgs e)
        {
            EventListDetail.Items.Clear() ;
            int index = SwimData.SelectedCells[0].RowIndex;
            SwimMeet sm = swimMeets[index];

            foreach (Event ev in sm.Events)
            {
                EventListDetail.Items.Add(ev.ToString());
            }
        }

        private void ShowSwimmersInEvent_Click(object sender, EventArgs e)
        {
            EventSwimmerList.Items.Clear();
            EventSwimData.DataSource = null;
            int index = SwimData.SelectedCells[0].RowIndex;
            SwimMeet sm = swimMeets[index];
            try
            {
                string eventName = EventListDetail.SelectedItem.ToString();
                foreach (Event ev in sm.Events)
                {
                    if (ev.ToString() == eventName)
                    {
                        EventSwimData.DataSource = ev.Swims;
                        EventSwimData.MultiSelect = false;
                        EventSwimData.ReadOnly = true;
                        foreach (Registrant re in ev.Swimmers)
                        {
                            EventSwimmerList.Items.Add(re.Name);
                        }
                    }
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, ERROR, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TimeClear_Click(object sender, EventArgs e)
        {
            TimeSpanInput.Text = "";
        }

        private void nOfLanes_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
