import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormControlName, FormGroup, Validators } from '@angular/forms';
import { PublisherApiService } from 'src/app/core/apiService/publisher-api.service';
import { CommonService } from 'src/app/core/services/common.service';

@Component({
  selector: 'app-add-edit-pubilisher',
  templateUrl: './add-edit-pubilisher.component.html',
  styleUrls: ['./add-edit-pubilisher.component.scss']
})
export class AddEditPubilisherComponent implements OnInit {
  publisherForm: FormGroup;
  message: string;
  constructor(private formBuilder: FormBuilder,private commonService: CommonService,private publisherApiService: PublisherApiService) { }

  ngOnInit(): void {
    this.publisherForm = this.formBuilder.group({
      firstName: new FormControl('',[Validators.required]),
      lastName: new FormControl('',[Validators.required]),
      groupNumber: new FormControl('',[Validators.required]),
      congregationId: new FormControl(1),
      isElder: new FormControl(false),
      isMs: new FormControl(false),
      isRp: new FormControl(false),
      isUnBaptized: new FormControl(false),
    });
  }

  async submit() {
    if(this.publisherForm.valid){
      this.commonService.toggleLoadingScreen();
      var result = await this.publisherApiService.addedit(this.publisherForm.value);
      this.commonService.toggleLoadingScreen();
      if(result.isSuccess){
        this.commonService.dismissDialog();
      } else {
        this.message = result.message;
      }
    } else {
      this.publisherForm.markAllAsTouched();
    }
  }
}
