module AddContact

open System
open System.IO

// File path to save contacts
let filePath = "Data/contacts.txt"

// Function to save a new contact to the file
let addContact name phoneNumber email =
    if String.IsNullOrWhiteSpace(name) || String.IsNullOrWhiteSpace(phoneNumber) || String.IsNullOrWhiteSpace(email) then
        printfn "All fields are required!"
    else
        // Create contact string
        let contact = $"{name},{phoneNumber},{email}"
        
        // Append to file
        try
            File.AppendAllText(filePath, contact + Environment.NewLine)
            printfn "Contact added successfully."
        with
        | ex -> printfn "An error occurred: %s" ex.Message

