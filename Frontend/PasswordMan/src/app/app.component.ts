import { NewPasswordDialogComponent } from './new-password-dialog/new-password-dialog.component';
import {MatDialog, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { OnInit } from '@angular/core';
import { ApiService } from './services/api.service';
import {AfterViewInit, Component, ViewChild} from '@angular/core';
import {MatPaginator} from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import {MatTableDataSource} from '@angular/material/table';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit{
  title = 'PasswordMan';
  displayedColumns: string[] = ['id','category', 'app', 'userName', 'action'];
  dataSource!: MatTableDataSource<any>;

  @ViewChild(MatPaginator) paginator!: MatPaginator;
  @ViewChild(MatSort) sort!: MatSort;

  constructor(private api:ApiService, private dialog: MatDialog) {}
  openDialog() {
    this.dialog.open(NewPasswordDialogComponent, {
      width:'30%'
    }).afterClosed()
    .subscribe(val=>{
      if (val==='save') {
        this.getAllPasswords();
      } 
    });
  }
  ngOnInit(): void {
    this.getAllPasswords();
  }

  editPassword(row:any){
    this.dialog.open(NewPasswordDialogComponent,{
      width:'30%',
      data:row
    }).afterClosed()
    .subscribe(val=>{
      if (val==='update') {
        this.getAllPasswords();
      } 
    });
  }
  deletePassword(id:number){
    if (confirm('Are you sure to delete this password?')){
      this.api.deletePassword(id)
    .subscribe({
      next:(res)=>{
        alert("Password deleted successfully");
        this.getAllPasswords();
      },
      error:()=>{
        alert("Error deleting password");
      }
    });
    }
  }
  getAllPasswords(){
    this.api.getAllPasswords()
    .subscribe({
      next:(res)=>{
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error:()=>{
        console.log("Error during fetching the records");
      }
    })
  }
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();

    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  refresh(){
    this.getAllPasswords();
    (<HTMLInputElement>document.getElementById("username")).value='';
    (<HTMLInputElement>document.getElementById("filterInput")).value='';
    
  }
  filterUsername(username:string){
    if (username===''){
      this.getAllPasswords();
    } else{
      this.api.getPasswordsByUsername(username,0)
    .subscribe({
      next:(res)=>{
        this.dataSource = new MatTableDataSource(res);
        this.dataSource.paginator = this.paginator;
        this.dataSource.sort = this.sort;
      },
      error:()=>{
        console.log("Error during fetching the records");
      }
    })
    }
    
  }
  clearFilterUsername(){
    
  }
}
