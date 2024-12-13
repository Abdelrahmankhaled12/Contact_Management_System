module AddContact

open System
open System.Drawing
open System.IO
open System.Windows.Forms

let createAddContactComponents (form: Form) (filePath: string) (updateList: string -> unit) =
    // Labels for inputs
    let nameLabel: Label = new Label(Text = "Name:", Left = 30, Top = 240, Width = 250, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    let phoneLabel: Label = new Label(Text = "Phone:", Left = 30, Top = 305, Width = 250, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    let emailLabel: Label = new Label(Text = "Email:", Left = 30, Top = 370, Width = 250, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    nameLabel.ForeColor <- Color.FromArgb(79, 70, 229)
    phoneLabel.ForeColor <- Color.FromArgb(79, 70, 229)
    emailLabel.ForeColor <- Color.FromArgb(79, 70, 229)

    // Inputs for adding a contact
    let nameInput: TextBox = new TextBox(Left = 30, Top = 270, Width = 390, Font = new Font("Arial", 12.0f))
    nameInput.PlaceholderText <- "Please enter name"

    let phoneInput: TextBox = new TextBox(Left = 30, Top = 335, Width = 390, Font = new Font("Arial", 12.0f))
    phoneInput.PlaceholderText <- "Please enter phone (e.g., 01012345678)"

    let emailInput: TextBox = new TextBox(Left = 30, Top = 400, Width = 390, Font = new Font("Arial", 12.0f))
    emailInput.PlaceholderText <- "Please enter email"

    // Button to save the contact
    let saveButton: Button = new Button(Text = "Add Contact", Width = 390, Height = 40, Left = 30, Top = 440, ForeColor = Color.White, FlatStyle = FlatStyle.Flat)
    saveButton.Font <- new Font("Arial", 14.0f, FontStyle.Bold)
    saveButton.BackColor <- Color.FromArgb(79, 70, 229)

    // Save contact
    saveButton.Click.Add(fun _ ->
        let name: string = nameInput.Text
        let phone: string = phoneInput.Text
        let email: string = emailInput.Text

        let isValidPhone (phone: string): bool =
            phone.StartsWith("010") || phone.StartsWith("011") || phone.StartsWith("012") || phone.StartsWith("015") && phone.Length = 11

        let isValidEmail (email: string): bool =
            email.Contains("@") && email.Contains(".")

        if String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(phone) || String.IsNullOrWhiteSpace(email) then
            MessageBox.Show("All fields are required!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        elif not (isValidPhone phone) then
            MessageBox.Show("Invalid phone number! It must start with 010, 011, 012, or 015.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        elif not (isValidEmail email) then
            MessageBox.Show("Invalid email address!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error) |> ignore
        else
            let newContact: string = $"{name}, {phone}, {email}"
            File.AppendAllText(filePath, $"{newContact}\n")
            MessageBox.Show("Contact added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information) |> ignore
            updateList ""
            nameInput.Text <- ""
            phoneInput.Text <- ""
            emailInput.Text <- ""
    )

    // Add components to the form
    form.Controls.Add(nameLabel :> Control)
    form.Controls.Add(nameInput :> Control)
    form.Controls.Add(phoneLabel :> Control)
    form.Controls.Add(phoneInput :> Control)
    form.Controls.Add(emailLabel :> Control)
    form.Controls.Add(emailInput :> Control)
    form.Controls.Add(saveButton :> Control)
