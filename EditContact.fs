module EditContact

open System
open System.Drawing
open System.Windows.Forms

let editContact (contact: string) 
                (updateContactInFile: string -> string -> unit) 
                (nameLabel: Label) 
                (phoneLabel: Label) 
                (emailLabel: Label) =
    let editForm: Form = new Form(Text = "Edit Contact", Width = 400, Height = 300)
    let nameInput: TextBox = new TextBox(Text = nameLabel.Text, Left = 20, Top = 20, Width = 350)
    let phoneInput: TextBox = new TextBox(Text = phoneLabel.Text, Left = 20, Top = 70, Width = 350)
    let emailInput: TextBox = new TextBox(Text = emailLabel.Text, Left = 20, Top = 120, Width = 350)
    let saveButton: Button = new Button(Text = "Save", Width = 100, Height = 40, Left = 150, Top = 180, BackColor = Color.Green, ForeColor = Color.White, FlatStyle = FlatStyle.Flat)

    saveButton.Click.Add(fun _ ->
        let newName: string = nameInput.Text
        let newPhone: string = phoneInput.Text
        let newEmail: string = emailInput.Text

        let isValidPhone (phone: string) =
            phone.StartsWith("010") || phone.StartsWith("011") || phone.StartsWith("012") || phone.StartsWith("015")
        let isValidEmail (email: string) = email.Contains("@") && email.Contains(".")

        if String.IsNullOrWhiteSpace(newName) || String.IsNullOrWhiteSpace(newPhone) || String.IsNullOrWhiteSpace(newEmail) then
            MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        elif not (isValidPhone newPhone) then
            MessageBox.Show("Invalid phone number! It must start with 010, 011, 012, or 015.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        elif not (isValidEmail newEmail) then
            MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        else
            let newContact: string = $"{newName}, {newPhone}, {newEmail}"
            updateContactInFile contact newContact
            nameLabel.Text <- newName
            phoneLabel.Text <- newPhone
            emailLabel.Text <- newEmail
            editForm.Close()
    )

    editForm.Controls.Add(nameInput)
    editForm.Controls.Add(phoneInput)
    editForm.Controls.Add(emailInput)
    editForm.Controls.Add(saveButton)
    editForm.Show()
