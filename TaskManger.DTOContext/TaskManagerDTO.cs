using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManger.DTOContext
{
    public class TaskMangerContext
    {
        public int Task_ID { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Task { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> IsTaskEnded { get; set; }
        public string Start_Date { get; set; }
        public string End_Date { get; set; }
        public string ParentTask { get; set; }
    }
    public class ParentTaskMangerContext
    {
        public int Id { get; set; }
        public Nullable<int> Parent_ID { get; set; }
        public string Parent_Task { get; set; }
    }
}
