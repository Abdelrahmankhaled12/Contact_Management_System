module SearchContact

open System.Windows.Forms
open System.Drawing

let createSearchComponents (updateList: string -> unit) =
    let searchLabel = new Label(Text = "Search for Contacts:", Width = 350, Top = 10, Left = 20, Font = new Font("Arial", 14.0f, FontStyle.Bold))
    searchLabel.ForeColor <- Color.FromArgb(79, 70, 229)

    let searchBox = new TextBox(Width = 478, Height = 28, Top = 38, Left = 25, Font = new Font("Arial", 12.0f))
    searchBox.PlaceholderText <- "Enter contact name or phone"

    let searchButton = new Button(Width = 40, Height = 28, Top = 38, Left = 500, BackColor = Color.FromArgb(245, 245, 253), FlatStyle = FlatStyle.Flat)
    searchButton.BackgroundImage <- Image.FromFile(@"D:\contact_system\contact_system\contact_system\Icons\search.png")
    searchButton.BackgroundImageLayout <- ImageLayout.Zoom
    searchButton.FlatAppearance.BorderSize <- 0

    searchButton.Click.Add(fun _ ->
        updateList searchBox.Text
    )

    (searchLabel, searchBox, searchButton)
