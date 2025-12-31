import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatDividerModule } from '@angular/material/divider';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { ActivatedRoute, Router } from '@angular/router';
import { WidgetsService } from '../../../services/widgets.service';

@Component({
  selector: 'app-widget-install',
  imports: [MatFormFieldModule, MatIconModule, MatInputModule, MatDividerModule, MatButtonModule, ReactiveFormsModule],
  templateUrl: './widget-install.html',
  styleUrl: './widget-install.scss',
})
export class WidgetInstall {

  protected form: FormGroup;
  private selectedFile: File | null = null;

  constructor(
    private readonly router: Router,
    private readonly route: ActivatedRoute,
    private readonly fb: FormBuilder,
    private readonly widgetsService: WidgetsService
  ) {
    this.form = this.fb.group({
      filename: ''
    });
  }

  onGobackClick() {
    this.router.navigate(['..'], { relativeTo: this.route });
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (!input.files || input.files.length === 0) {
      return;
    }

    this.selectedFile = input.files[0];
    this.form.patchValue({
      filename: input.files[0].name
    });
    
    this.form.updateValueAndValidity({emitEvent: false});
    this.form.markAsDirty();
  }

  onSubmitForm() {
    if (!this.selectedFile) {
      return;
    }

    const formData = new FormData();
    formData.append('file', this.selectedFile);

    this.widgetsService.install(formData).subscribe(() => {
      this.form.patchValue({
        filename: ''
      });
      this.form.markAsPristine();
      this.selectedFile = null;
    });
  }
}
