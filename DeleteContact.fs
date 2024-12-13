module DeleteContact

open System.Windows.Forms
open System.Drawing

let deleteContact (contact: string) 
                  (contactsPanel: FlowLayoutPanel) 
                  (deleteContactFromFile: string -> unit) 
                  (panel: Panel) =
    let result = MessageBox.Show("Are you sure you want to delete this contact?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
    if result = DialogResult.Yes then
        contactsPanel.Controls.Remove(panel) // Remove from UI
        deleteContactFromFile contact // Remove from file
