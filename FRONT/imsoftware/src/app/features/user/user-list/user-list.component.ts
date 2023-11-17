import { AfterViewInit, Component, ViewChild, OnInit } from '@angular/core';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { User } from '../models/user.model';
import { UserService } from '../services/user.service';
import { MatDialog } from '@angular/material/dialog';
import { AddEditUserComponent } from '../add-edit-user/add-edit-user.component';
import { DeleteUserComponent } from '../delete-user/delete-user.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.css']
})
export class UserListComponent implements AfterViewInit, OnInit {
  displayedColumns: string[] = ['Nombre', 'Edad', 'Email', 'Acciones'];
  dataSource = new MatTableDataSource<User>();

  constructor(
    private userService: UserService,
    public dialog: MatDialog,
    private snackBar: MatSnackBar
  ){}

  ngOnInit(): void {
    this.showUsers();
  }

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  ngAfterViewInit() {
    this.dataSource.paginator = this.paginator;
  }

  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  showUsers(){
    this.userService.getList().subscribe({
      next:(data) => {
        if(data.status)
          this.dataSource.data = data.value
        else
          this.showAlert(data.msg, "Error")
      }, error:(e) =>{}
    })
  }

  showAlert(msg: string, action: string){
    this.snackBar.open(msg,action,{
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    })
  }

  modalAdd() {
    this.dialog.open(AddEditUserComponent, {
      disableClose: true,
      width: "350px"
    }).afterClosed().subscribe(response => {
      if(response === "Creado"){
        this.showUsers()
      }
    });
  }

  modalEdit(data: User){
    this.dialog.open(AddEditUserComponent, {
      disableClose: true,
      data: data
    }).afterClosed().subscribe(response => {
      if(response === "Editado"){
        this.showUsers()
      }
    });
  }

  modalDelete(data: User){
    this.dialog.open(DeleteUserComponent, {
      disableClose: true,
      width: "350px",
      data: data
    }).afterClosed().subscribe(response => {
      if(response == "Eliminar"){
        this.userService.delete(data.id).subscribe({
          next: (data) => {
            if(data.status){
              this.showAlert("Usuario eliminado", "Listo")
              this.showUsers()
            } else {
              this.showAlert(data.msg, "Error")
            }
          },
          error: (error) =>{
            console.log(error)
          }
        })
      }
    });
  }
}
