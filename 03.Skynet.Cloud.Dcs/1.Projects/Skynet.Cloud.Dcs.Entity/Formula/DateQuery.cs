using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace UWay.Skynet.Cloud.Dcs.Entity
{
    
    public class DateQuery
    {
        private string distimepara;

        
        public string Distimepara
        {
            get { return distimepara; }
            set { distimepara = value; }
        }
        private string disyear;

        
        public string Disyear
        {
            get { return disyear; }
            set { disyear = value; }
        }
        private string timeType;

        
        public string TimeType
        {
            get { return timeType; }
            set { timeType = value; }
        }

        private List<string> showHourField;

        
        public List<string> ShowHourField
        {
            get { return showHourField; }
            set { showHourField = value; }
        }
        private string parabeginweek;

        
        public string Parabeginweek
        {
            get { return parabeginweek; }
            set { parabeginweek = value; }
        }
        private string paraendweek;

        
        public string Paraendweek
        {
            get { return paraendweek; }
            set { paraendweek = value; }
        }
        private string theweek;

        
        public string TheWeek
        {
            get { return theweek; }
            set { theweek = value; }
        }
        private string sunday;

        
        public string Sunday
        {
            get { return sunday; }
            set { sunday = value; }
        }
        private string saturday;

        
        public string Saturday
        {
            get { return saturday; }
            set { saturday = value; }
        }
        private string timefield;

        
        public string Timefield
        {
            get { return timefield; }
            set { timefield = value; }
        }
        private string byType;

        
        public string ByType
        {
            get { return byType; }
            set { byType = value; }
        }
        private string insql;

        
        public string Insql
        {
            get { return insql; }
            set { insql = value; }
        }
        private string weeksql;

        
        public string Weeksql
        {
            get { return weeksql; }
            set { weeksql = value; }
        }
    }
}
