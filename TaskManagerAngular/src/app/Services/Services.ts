import { Injectable } from '@angular/core';
import { taskData, Task } from '../Services/Model';
import { Http, Headers, RequestOptions } from '@angular/http';


@Injectable({
  providedIn: 'root'
})

export class SharedService {

  taskdata: any;
  taskList: any;

constructor(private httpServ: Http) { }
  getTaskListUri = "http://localhost:50451/api/Task/GetTaskList";

  GetTaskList(){
    return this.httpServ.get(this.getTaskListUri);
  }

  getTaskUri = "http://localhost:50451/api/Task/GetTaskById";
  GetTaskById(Id:number){
    return this.httpServ.get(this.getTaskUri+ "?Id="+Id);
  }

  CreateTaskUri = "http://localhost:50451/api/Task/CreateTask";
  AddTask(data: Task) {
    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });
    return this.httpServ.post(this.CreateTaskUri, data, options);
  }

  updateTaskUri = "http://localhost:50451/api/Task/UpdateTask";
  UpdateTask(data: Task) {

    const headers = new Headers({ 'Content-Type': 'application/json' });
    const options = new RequestOptions({ headers: headers });

    return this.httpServ.post(this.updateTaskUri, data, options);
  }

  endTaskUri = "http://localhost:50451/api/Task/EndTask?Id=";
  EndTask(Id:number){
    return this.httpServ.get(this.endTaskUri+Id);
  }

  getParentTaskUri = "http://localhost:50451/api/Task/ParentTask";
  GetParentTask(){
    return this.httpServ.get(this.getParentTaskUri);
  }

}