using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TaskManager.DataLayer;

using TaskManger.DTOContext;

namespace TaskManager.BusinessLayer
{
    public class TaskmanagerBL
    {
        private DataAccess dataAccess = new DataAccess();

        #region
        /// <summary>
        /// GetTaskList
        /// </summary>
        /// <returns></returns>
        public List<TaskMangerContext> GetTaskList()
        {
              var taskList = dataAccess.GetTaskList();

            List<TaskMangerContext> taskListData = new List<TaskMangerContext>();
            foreach (var item in taskList)
            {
                TaskMangerContext tmContext = new TaskMangerContext();
                tmContext.Task_ID = item.Task_ID;
                tmContext.Parent_ID = item.Parent_ID;
                tmContext.Task = item.Task;
                tmContext.StartDate = item.StartDate;
                tmContext.EndDate = item.EndDate.Value;
                tmContext.Priority = item.Priority;
                tmContext.IsTaskEnded = item.IsTaskEnded == null ? 0 : item.IsTaskEnded;
                tmContext.ParentTask = item.ParentTask;
                taskListData.Add(tmContext);
            }
            return taskListData;
        }
        #endregion

        #region
        /// <summary>
        /// AddTask
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parentTask"></param>
        /// <returns></returns>
        public bool AddTask(TaskMangerContext task, ParentTaskMangerContext parentTask)
        {
            bool IsTaskAdded;

            Task_Master tmContext = new Task_Master();
            tmContext.Task_ID = task.Task_ID;
            tmContext.Parent_ID = Convert.ToInt32(task.ParentTask);
            tmContext.Task = task.Task;
            tmContext.StartDate = task.StartDate;
            tmContext.EndDate = task.EndDate;
            tmContext.Priority = task.Priority;
            tmContext.IsTaskEnded = task.IsTaskEnded ;
            tmContext.ParentTask = task.ParentTask;

            ParentTask_Master pmContext = new ParentTask_Master();
            //pmContext.Parent_ID =(int) parentTask.Parent_ID;
            pmContext.Parent_Task = parentTask.Parent_Task;

            IsTaskAdded = dataAccess.AddTask(tmContext, pmContext);
            return IsTaskAdded;
        }
        #endregion

        #region
        /// <summary>
        /// UpdateTask
        /// </summary>
        /// <param name="task"></param>
        /// <param name="parentTask"></param>
        /// <returns></returns>
        public bool UpdateTask(TaskMangerContext task, ParentTaskMangerContext parentTask)
        {
            Task_Master tmContext = new Task_Master();
            tmContext.Task_ID = task.Task_ID;
            tmContext.Parent_ID = task.Parent_ID;
            tmContext.Task = task.Task;
            tmContext.StartDate = task.StartDate;
            tmContext.EndDate = task.EndDate;
            tmContext.Priority = task.Priority;
            tmContext.IsTaskEnded = task.IsTaskEnded;
            tmContext.ParentTask = task.ParentTask;

            ParentTask_Master pmContext = new ParentTask_Master();
            pmContext.Parent_ID =(int)task.Task_ID;
            pmContext.Parent_Task = task.Task;
            bool IsTaskUpdated;
            IsTaskUpdated = dataAccess.UpdateTask(tmContext, pmContext);
            return IsTaskUpdated;
        }
        #endregion

        #region
        /// <summary>
        /// EndTask
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public bool EndTask(int taskId)
        {
            bool IsTaskEnded;
            IsTaskEnded = dataAccess.EndTask(taskId);
            return IsTaskEnded;

        }
        #endregion

        #region
        /// <summary>
        /// GetTaskById
        /// </summary>
        /// <param name="taskId"></param>
        /// <returns></returns>
        public TaskMangerContext GetTaskById(int taskId)
        {
            TaskMangerContext taskData = new TaskMangerContext();
            Task_Master task = (dataAccess.GetTaskById(taskId));
            taskData.Task_ID = task.Task_ID;
            taskData.Parent_ID = task.Parent_ID;
            taskData.Task = task.Task;
            taskData.StartDate = task.StartDate;
            taskData.EndDate = task.EndDate;
            taskData.Priority = task.Priority;
            taskData.IsTaskEnded = task.IsTaskEnded;
            taskData.ParentTask = task.ParentTask;
            taskData.Start_Date = task.StartDate.Value.ToShortDateString();
            taskData.End_Date = task.EndDate.Value.ToShortDateString();
            return taskData;
        }
        #endregion

        #region 
        /// <summary>
        /// ParentTask
        /// </summary>
        /// <returns></returns>
        public List<ParentTaskMangerContext> ParentTask()
        {
            List<ParentTask_Master> ParentTaskList;
            ParentTaskList = dataAccess.ParentTask();
            List<ParentTaskMangerContext> pmContext = (from ptlist in ParentTaskList
                                                       select new ParentTaskMangerContext
                                                       {
                                                           Parent_ID = ptlist.Parent_ID,
                                                           Parent_Task = ptlist.Parent_Task
                                                       }).ToList();

            return pmContext;
        }
        #endregion
    }
}
