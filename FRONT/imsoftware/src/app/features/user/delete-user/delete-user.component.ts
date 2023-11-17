import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from '../models/user.model';

@Component({
  selector: 'app-delete-user',
  templateUrl: './delete-user.component.html',
  styleUrls: ['./delete-user.component.css']
})
export class DeleteUserComponent implements OnInit {
  constructor(
    private dialogReference: MatDialogRef<DeleteUserComponent>,
    @Inject(MAT_DIALOG_DATA) public dataUser: User
  ){

  }

  ngOnInit(): void {
  }

  confirmDelete(){
    if(this.dataUser){
      this.dialogReference.close("Eliminar")
    }
  }
}
