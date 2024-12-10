module DeleteContact

open System
open System.IO

// File path to save contacts
let filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "contacts.txt")

// Function to delete a contact by phone number
let deleteContact phoneNumber =
    if not (File.Exists(filePath)) then
        printfn "Contacts file not found."
    else
        // Read all contacts from the file
        let allContacts = File.ReadAllLines(filePath) |> Array.toList
        
        // Filter out the contact with the given phone number
        let updatedContacts = 
            allContacts 
            |> List.filter (fun contact -> 
                let fields = contact.Split(',')
                if fields.Length > 1 then fields.[1] <> phoneNumber
                else true
            )
        
        if List.length updatedContacts = List.length allContacts then
            printfn "Contact with phone number %s not found." phoneNumber
        else
            // Write updated contacts back to the file
            File.WriteAllLines(filePath, updatedContacts)
            printfn "Contact with phone number %s deleted successfully." phoneNumber

