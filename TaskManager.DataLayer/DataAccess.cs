using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.DataLayer
{
    public class DataAccess
    {
        private readonly Task_ManagerEntities dbContext = new Task_ManagerEntities();

        #region GetTaskList

       
        public List<Task_Master> GetTaskList()
        {
            List<Task_Master> taskList = new List<Task_Master>();
            try
            {
var Query = (from task in dbContext.Task_Master join Ptask in dbContext.ParentTask_Master on task.Parent_ID equals Ptask.Parent_ID
                            into td   from Task_Masters in td.DefaultIfEmpty()                       
                            select new 
                            {
                                Task_ID = task.Task_ID,
                                Parent_ID = task.Parent_ID,
                                Task = task.Task,
                                StartDate = task.StartDate,
                                EndDate = task.EndDate,
                                Priority = task.Priority,
                                IsTaskEnded = task.IsTaskEnded,
                                ParentTask = Task_Masters.Parent_Task
                            }).Distinct().ToList();

                taskList = Query.ToList().Select(r => new Task_Master
                {
                    Task_ID = r.Task_ID,
                    Parent_ID = r.Parent_ID,
                    Task = r.Task,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Priority = r.Priority,
                    IsTaskEnded = r.IsTaskEnded,
                    ParentTask = r.ParentTask
                }).ToList();
            }
            catch (Exception ex)
            {
               
            }
            return taskList;
        }
        #endregion


        #region AddTask
        public bool AddTask(Task_Master task,ParentTask_Master parentTask)
        {
            try
            {             
           
                var addTask = dbContext.Task_Master.Add(task);
                var addParentTask = dbContext.ParentTask_Master.Add(parentTask);
                
                dbContext.SaveChanges();
        
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region UpdateTask
        public bool UpdateTask(Task_Master task, ParentTask_Master parentTask)
        {
            try
            {
                Task_Master existTask = dbContext.Task_Master.Find(task.Task_ID);
                ((IObjectContextAdapter)dbContext).ObjectContext.Detach(existTask);

                dbContext.Entry(task).State = EntityState.Modified;
                dbContext.SaveChanges();

                ParentTask_Master existPTask = dbContext.ParentTask_Master.Find(parentTask.Parent_ID);
                ((IObjectContextAdapter)dbContext).ObjectContext.Detach(existPTask);

                dbContext.Entry(parentTask).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion


        #region EndTask
        public bool EndTask(int taskId)
        {
            try
            {
                Task_Master taskData = dbContext.Task_Master.Find(taskId);

                taskData.IsTaskEnded = 1;
                dbContext.Entry(taskData).State = EntityState.Modified;
                dbContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region GetTaskById
        public Task_Master GetTaskById(int taskId)
        {
            Task_Master taskData = new Task_Master();
            try
            {
                // taskData = dbContext.Task_Master.Find(taskId);
                var Query = (from task in dbContext.Task_Master
                             join Ptask in dbContext.ParentTask_Master on task.Parent_ID equals Ptask.Parent_ID 
                             into td  from Task_Masters in td.DefaultIfEmpty()
                             where task.Task_ID == taskId
                             select new
                             {
                                 Task_ID = task.Task_ID,
                                 Parent_ID = task.Parent_ID,
                                 Task = task.Task,
                                 StartDate = task.StartDate,
                                 EndDate = task.EndDate,
                                 Priority = task.Priority,
                                 IsTaskEnded = task.IsTaskEnded,
                                 ParentTask = Task_Masters.Parent_Task
                             }).Distinct().ToList();

                taskData = Query.ToList().Select(r => new Task_Master
                {
                    Task_ID = r.Task_ID,
                    Parent_ID = r.Parent_ID,
                    Task = r.Task,
                    StartDate = r.StartDate,
                    EndDate = r.EndDate,
                    Priority = r.Priority,
                    IsTaskEnded = r.IsTaskEnded,
                    ParentTask = r.ParentTask
                }).FirstOrDefault();
                return taskData;
            }
            catch (Exception ex)
            {
                return taskData;
            }
        }
        #endregion

        #region ParentTask
        public List<ParentTask_Master> ParentTask()
        {
            List<ParentTask_Master> parentTaskList = new List<DataLayer.ParentTask_Master>();

            try
            {

               var PTask = (from task in dbContext.Task_Master select new { Id = task.Task_ID, Parent_Task = task.Task }).ToList();
               foreach (var data in PTask) {
                    ParentTask_Master ptask = new ParentTask_Master();
                    ptask.Parent_ID = data.Id;
                    ptask.Parent_Task = data.Parent_Task;
                    parentTaskList.Add(ptask);
                }
                return parentTaskList;
            }
            catch (Exception ex)
            {
                return parentTaskList;
            }
        }
        #endregion

    }
}
