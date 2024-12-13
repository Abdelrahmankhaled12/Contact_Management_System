open System
open System.Drawing
open System.IO
open System.Windows.Forms
open SearchContact
open EditContact
open DeleteContact
open AddContact

[<EntryPoint>]
let main argv =
    // File path for storing contacts
    let filePath: string = @"D:\contact_system\contact_system\contact_system\Data\contacts.txt"

    // Create the main form
    let form: Form = new Form(Text = "Contact Management System", Width = 1000, Height = 600)
    form.BackColor <- Color.FromArgb(245, 245, 253)

    // Add a logo to the left side
    let logo: PictureBox = new PictureBox(Width = 250, Height = 250, Left = 90, Top = 0, Image = Image.FromFile(@"D:\contact_system\contact_system\contact_system\Icons\logo.png"), SizeMode = PictureBoxSizeMode.Zoom)

    // Panel for displaying contacts
    let sidePanel: Panel = new Panel(Width = 550, Dock = DockStyle.Right, BackColor = Color.FromArgb(245, 245, 253))

    // Panel to display contact list
    let contactsPanel: FlowLayoutPanel = new FlowLayoutPanel(Top = 70, Left = 20, Width = 525, Height = 500, AutoScroll = true)
    contactsPanel.BackColor <- Color.FromArgb(245, 245, 253)

    // Load contacts from file
    let loadContacts (): string list =
        if File.Exists(filePath) then
            File.ReadAllLines(filePath)
            |> Array.toList
        else
            []

    // Delete a contact from the file
    let deleteContactFromFile (contactToDelete: string): unit =
        let contacts = loadContacts()
        let updatedContacts = contacts |> List.filter (fun contact -> contact <> contactToDelete)
        File.WriteAllLines(filePath, updatedContacts)

    // Update a contact in the file
    let updateContactInFile (oldContact: string) (newContact: string): unit =
        let contacts = loadContacts()
        let updatedContacts = contacts |> List.map (fun contact -> if contact = oldContact then newContact else contact)
        File.WriteAllLines(filePath, updatedContacts)

    // Create contact item
    let createContactItem (contact: string): Panel =
        let panel: Panel = new Panel(Width = 495, Height = 80, BackColor = Color.White, Margin = Padding(5))
        let icon = new PictureBox(Width = 50, Height = 50, Left = 10, Top = 15, Image = Image.FromFile(@"D:\contact_system\contact_system\contact_system\Icons\user.png"), SizeMode = PictureBoxSizeMode.Zoom)
        let nameLabel: Label = new Label(Text = contact.Split(',').[0].Trim(), Left = 70, Top = 10, Width = 300, Font = new Font("Arial", 14.0f, FontStyle.Bold))
        nameLabel.ForeColor <- Color.FromArgb(79, 70, 229)
        let emailLabel: Label = new Label(Text = contact.Split(',').[2].Trim(), Left = 70, Top = 31, Width = 300, Font = new Font("Arial", 8.0f))
        let phoneLabel: Label = new Label(Text = contact.Split(',').[1].Trim(), Left = 70, Top = 51, Width = 300, Font = new Font("Arial", 12.0f))
        let deleteButton: Button = new Button(Width = 30, Height = 30, Left = 415, Top = 25, BackColor = Color.Transparent, FlatStyle = FlatStyle.Flat)
        deleteButton.BackgroundImage <- Image.FromFile(@"D:\contact_system\contact_system\contact_system\Icons\delete.png")
        deleteButton.BackgroundImageLayout <- ImageLayout.Zoom
        deleteButton.FlatAppearance.BorderSize <- 0
        deleteButton.Click.Add(fun _ -> deleteContact contact contactsPanel deleteContactFromFile panel)

        let editButton: Button = new Button(Width = 30, Height = 30, Left = 450, Top = 25, BackColor = Color.Transparent, FlatStyle = FlatStyle.Flat)
        editButton.BackgroundImage <- Image.FromFile(@"D:\contact_system\contact_system\contact_system\Icons\edit.png")
        editButton.BackgroundImageLayout <- ImageLayout.Zoom
        editButton.FlatAppearance.BorderSize <- 0
        editButton.Click.Add(fun _ -> editContact contact updateContactInFile nameLabel phoneLabel emailLabel)

        panel.Controls.Add(icon)
        panel.Controls.Add(nameLabel)
        panel.Controls.Add(emailLabel)
        panel.Controls.Add(phoneLabel)
        panel.Controls.Add(deleteButton)
        panel.Controls.Add(editButton)
        panel

    // Update the contact list
    let updateList (filterText: string): unit =
        contactsPanel.Controls.Clear()
        let contacts = loadContacts()
        let filteredContacts =
            if String.IsNullOrWhiteSpace(filterText) then contacts
            else contacts |> List.filter (fun contact -> contact.ToLower().Contains(filterText.ToLower()))
        filteredContacts |> List.iter (fun contact -> contactsPanel.Controls.Add(createContactItem contact))

    // Add Contact inputs below the logo
    AddContact.createAddContactComponents form filePath updateList

    // Create search components
    let (searchLabel, searchBox, searchButton) = createSearchComponents updateList

    // Add components to the panel
    sidePanel.Controls.Add(searchLabel)
    sidePanel.Controls.Add(searchBox)
    sidePanel.Controls.Add(searchButton)
    sidePanel.Controls.Add(contactsPanel)

    // Add components to the form
    form.Controls.Add(sidePanel)
    form.Controls.Add(logo)

    // Load contacts on startup
    updateList ""

    // Run the application
    Application.Run(form)
    0
