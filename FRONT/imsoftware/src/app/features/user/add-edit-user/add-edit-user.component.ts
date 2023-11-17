import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddUserRequest } from '../models/add-user-request.model';
import { UserService } from '../services/user.service';
import { User } from '../models/user.model';


@Component({
  selector: 'app-add-edit-user',
  templateUrl: './add-edit-user.component.html',
  styleUrls: ['./add-edit-user.component.css']
})
export class AddEditUserComponent implements OnInit {
  formUser: FormGroup
  titleAction: string = "Nuevo"
  buttonAction: string = "Guardar"
  listUsers: AddUserRequest[] = []
  messageErrorName: any
  messageErrorAge: any
  messageErrorEmail: any

  constructor(
    private dialogReference: MatDialogRef<AddEditUserComponent>,
    private fb: FormBuilder,
    private snackBar: MatSnackBar,
    private userService: UserService,
    @Inject(MAT_DIALOG_DATA) public dataUser: User
  ){
    this.formUser = this.fb.group({
      id: [''],
      name: ['', Validators.required],
      age: ['', Validators.required],
      email: ['', Validators.required],
    })
  }

  showAlert(msg: string, action: string){
    this.snackBar.open(msg,action,{
      horizontalPosition: "end",
      verticalPosition: "top",
      duration: 3000
    })
  }

  addEditUser(){
    this.messageErrorName = ""
    this.messageErrorAge = ""
    this.messageErrorEmail = ""

    if(this.dataUser == null){
      const model: AddUserRequest = {
        name: this.formUser.value.name,
        age: this.formUser.value.age,
        email: this.formUser.value.email
      }
      this.userService.addUser(model).subscribe({
        next:(data) => {
          if(data.status){
            this.showAlert("Usuario creado", "Listo");
            this.dialogReference.close("Creado");
          } else {
            const error = JSON.parse(data.msg);
            if(error.Name){
              this.messageErrorName = error.Name
            }
            if(error.Age){
              this.messageErrorAge = error.Age
            }
            if(error.Email){
              this.messageErrorEmail = error.Email
            }
            this.showAlert("Usuario no creado", "Error");
          }
        }, error:(error) => {
          console.log(error)
          this.showAlert("Usuario no creado", "Error");
        }
      })
    } else {
      const model: User = {
        id: this.formUser.value.id,
        name: this.formUser.value.name,
        age: this.formUser.value.age,
        email: this.formUser.value.email
      }
      this.userService.update(model).subscribe({
        next:(data) => {
          if(data.status){
            this.showAlert("Usuario editado", "Listo")
            this.dialogReference.close("Editado")
          } else {
            const error = JSON.parse(data.msg);
            if(error.Name){
              this.messageErrorName = error.Name
            }
            if(error.Age){
              this.messageErrorAge = error.Age
            }
            if(error.Email){
              this.messageErrorEmail = error.Email
            }
            this.showAlert("Usuario no fue editado", "Error")
          }
        }, error:(error) => {
          console.log(error)
          this.showAlert("Usuario no fue editado", "Error")
        }
      })
    }
  }

  ngOnInit(): void {
    if(this.dataUser){
      this.formUser.patchValue({
        id: this.dataUser.id,
        name: this.dataUser.name,
        age: this.dataUser.age,
        email: this.dataUser.email,
      })
      this.titleAction = "Editar"
      this.buttonAction = "Actualizar"
    }
  }
}
