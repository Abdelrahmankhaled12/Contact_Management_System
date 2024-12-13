module EditContact

open System
open System.IO

// File path to save contacts
let filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "contacts.txt")

// Function to edit a contact's details by phone number
let editContact phoneNumber newName newEmail =
    if not (File.Exists(filePath)) then
        printfn "Contacts file not found."
    else
        // Read all contacts from the file
        let allContacts = File.ReadAllLines(filePath) |> Array.toList

        // Check if the contact exists
        let contactExists = 
            allContacts 
            |> List.exists (fun contact -> 
                let fields = contact.Split(',')
                if fields.Length > 1 then fields.[1] = phoneNumber
                else false
            )

        if not contactExists then
            printfn "No contact found with phone number %s" phoneNumber
        else
            // Update the contact's details
            let updatedContacts = 
                allContacts 
                |> List.map (fun contact -> 
                    let fields = contact.Split(',')
                    if fields.Length > 1 && fields.[1] = phoneNumber then
                        $"{newName},{phoneNumber},{newEmail}"
                    else
                        contact
                )

            // Write updated contacts back to the file
            File.WriteAllLines(filePath, updatedContacts)
            printfn "Contact with phone number %s updated successfully." phoneNumber

