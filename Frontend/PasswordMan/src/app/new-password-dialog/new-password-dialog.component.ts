import { Component, Inject } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { OnInit } from '@angular/core';
import { ApiService } from '../services/api.service';
import { MatDialogRef ,MAT_DIALOG_DATA} from '@angular/material/dialog';

@Component({
  selector: 'app-new-password-dialog',
  templateUrl: './new-password-dialog.component.html',
  styleUrls: ['./new-password-dialog.component.scss']
})
export class NewPasswordDialogComponent implements OnInit {
  passwordAdd: FormGroup = new FormGroup({
    category: new FormControl('',[Validators.required]),
    app: new FormControl('',[Validators.required]),
    userName: new FormControl('',[Validators.required,Validators.email]),
    encryptedPassword: new FormControl('', [Validators.required, Validators.min(3) ])
  });
  actionButton = "Save";
  constructor(private api:ApiService, private dialogRef: MatDialogRef<NewPasswordDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public editData: any,
    private formBuilder:FormBuilder
    ){}
  hide = true;
  get passwordInput() { return this.passwordAdd.get('password'); }

  ngOnInit(): void {
  
    if (this.editData){
      var id = this.editData.id;
      this.actionButton = "Update"
      this.api.getPasswordById(id,1)
      .subscribe(data=>{
        this.passwordAdd.controls["category"].setValue(data.category);
        this.passwordAdd.controls["app"].setValue(data.app);
        this.passwordAdd.controls["userName"].setValue(data.userName);
        this.passwordAdd.controls["encryptedPassword"].setValue(data.encryptedPassword);
      })
    }
  }



  addPassword(){
    if (!this.editData)
    {
      if (this.passwordAdd.valid)
      {
        this.api.postPassword(this.passwordAdd.value)
        .subscribe({
          next:(res)=>{
            alert("Password added successfully")
            this.passwordAdd.reset();
            this.dialogRef.close('save');
          },
          error:()=>{
            alert("Error while adding password")
          }
      })
      } else{
        alert("Please fill the form correctly")
      }
    }
    else
    {

      if (this.passwordAdd.valid)
      {
        this.updatePassword();
      } else{
        alert("Please fill the form correctly")
      }
    }
    
  }

  updatePassword(){
      this.api.updatePassword(this.editData.id,this.passwordAdd.value)
      .subscribe({
        next:(res)=>{
          alert("Password updated successfully")
          this.passwordAdd.reset();
          this.dialogRef.close('update');
        },
        error:()=>{
          alert("Error while updating password")
        }
      });
  }
}
