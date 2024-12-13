module EditContact

open System
open System.Drawing
open System.Windows.Forms

let editContact (contact: string) 
                (updateContactInFile: string -> string -> unit) 
                (nameLabelValue: Label) 
                (phoneLabelValue: Label) 
                (emailLabelValue: Label) =
    let editForm: Form = new Form(Text = "Edit Contact", Width = 400, Height = 300)
    editForm.BackColor <- Color.FromArgb(245, 245, 253)

    // Labels for inputs
    let nameLabel: Label = new Label(Text = "Name:", Left = 30, Top = 10, Width = 250, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    let phoneLabel: Label = new Label(Text = "Phone:", Left = 30, Top = 75, Width = 250, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    let emailLabel: Label = new Label(Text = "Email:", Left = 30, Top = 140, Width = 250, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    nameLabel.ForeColor <- Color.FromArgb(79, 70, 229)
    phoneLabel.ForeColor <- Color.FromArgb(79, 70, 229)
    emailLabel.ForeColor <- Color.FromArgb(79, 70, 229)

    // Inputs for editing a contact
    let nameInput: TextBox = new TextBox(Text = nameLabelValue.Text, Left = 30, Top = 40, Width = 300, Font = new Font("Arial", 12.0f))
    nameInput.PlaceholderText <- "Please enter name"

    let phoneInput: TextBox = new TextBox(Text = phoneLabelValue.Text, Left = 30, Top = 105, Width = 300, Font = new Font("Arial", 12.0f))
    phoneInput.PlaceholderText <- "Please enter phone (e.g., 01012345678)"

    let emailInput: TextBox = new TextBox(Text = emailLabelValue.Text, Left = 30, Top = 170, Width = 300, Font = new Font("Arial", 12.0f))
    emailInput.PlaceholderText <- "Please enter email"

    // Button to save the updated contact
    let saveButton: Button = new Button(Text = "Update Contact", Width = 300, Height = 40, Left = 30, Top = 210, ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    saveButton.Font <- new Font("Arial", 14.0f, FontStyle.Bold)
    saveButton.BackColor <- Color.FromArgb(79, 70, 229)

    saveButton.Click.Add(fun _ ->
        let newName: string = nameInput.Text
        let newPhone: string = phoneInput.Text
        let newEmail: string = emailInput.Text

        let isValidPhone (phone: string): bool =
            (phone.StartsWith("010") || phone.StartsWith("011") || phone.StartsWith("012") || phone.StartsWith("015")) && phone.Length = 11

        let isValidEmail (email: string): bool = email.Contains("@") && email.Contains(".")

        if String.IsNullOrWhiteSpace(newName) || String.IsNullOrWhiteSpace(newPhone) || String.IsNullOrWhiteSpace(newEmail) then
            MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        elif not (isValidPhone newPhone) then
            MessageBox.Show("Invalid phone number! It must start with 010, 011, 012, or 015 and have 11 digits.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        elif not (isValidEmail newEmail) then
            MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        else
            let newContact: string = $"{newName}, {newPhone}, {newEmail}"
            updateContactInFile contact newContact
            nameLabelValue.Text <- newName
            phoneLabelValue.Text <- newPhone
            emailLabelValue.Text <- newEmail
            editForm.Close()
    )

    // Add components to the edit form
    editForm.Controls.Add(nameLabel)
    editForm.Controls.Add(nameInput)
    editForm.Controls.Add(phoneLabel)
    editForm.Controls.Add(phoneInput)
    editForm.Controls.Add(emailLabel)
    editForm.Controls.Add(emailInput)
    editForm.Controls.Add(saveButton)

    // Show the edit form
    editForm.Show()
