import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { RouterLink, RouterOutlet } from '@angular/router';
import { InvitationItemComponent } from '../invitation-item/invitation-item.component';
import { InvitationService } from '../../services/invitation.service';
import { Invitation } from '../../models/Invitation';
import { NgForOf } from '@angular/common';

interface InvitationResponse {
  invitations: Invitation[];
}

@Component({
  selector: 'app-invitations-list',
  standalone: true,
  imports: [RouterOutlet, ReactiveFormsModule, RouterLink, InvitationItemComponent, NgForOf],
  templateUrl: './invitations-list.component.html',
  styleUrls: ['./invitations-list.component.css']
})


export class InvitationsListComponent implements OnInit{
  rows: Invitation[] = [];
  editableRows: Set<string> = new Set();
  
  
  constructor(private invitationsService: InvitationService) {}

  ngOnInit() {
    this.loadInvitations();
  }

  loadInvitations() {
    this.invitationsService.getInvitations(true).subscribe(editableData => {
      let editData = editableData as unknown as InvitationResponse;
      const invitations = editData.invitations;
      this.editableRows = new Set(invitations.map(row => row.id ?? ''));
  
      this.invitationsService.getInvitations(false).subscribe(data => {
        let allData = data as unknown as InvitationResponse;
        const combinedInvitations = [...invitations, ...allData.invitations];
        const uniqueInvitations = Array.from(new Set(combinedInvitations.map(inv => inv.id)))
          .map(id => combinedInvitations.find(inv => inv.id === id))
          .filter(inv => inv !== undefined) as Invitation[];
  
        this.rows = uniqueInvitations;
      });
    });
  }

  onSave(updatedRow: Invitation) {
    this.invitationsService.updateInvitationDeadline(updatedRow.id??'', updatedRow.deadline).subscribe(() => {
      this.loadInvitations();
    });
  }

  onDelete(row: Invitation) {
    this.invitationsService.deleteInvitation(row.id??'').subscribe(() => {
      this.rows = this.rows.filter(r => r.id !== row.id);
    });
  }

  isEditable(rowId: string): boolean {
    return this.editableRows.has(rowId);
  }
}
