module SearchContact

open System
open System.IO

// File path to save contacts
let filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "contacts.txt")

// Function to search contacts by name or phone number
let searchContacts (query: string) =
    if not (File.Exists(filePath)) then
        printfn "Contacts file not found."
    else
        // Read all contacts from the file
        let allContacts = File.ReadAllLines(filePath) |> Array.toList
        
        // Filter contacts matching the query
        let matchingContacts = 
            allContacts 
            |> List.filter (fun contact -> 
                let fields = contact.Split(',')
                if fields.Length > 1 then 
                    fields.[0].ToLower().Contains(query.ToLower()) || fields.[1].Contains(query)
                else false
            )
        
        // Display matching contacts
        if matchingContacts.IsEmpty then
            printfn "No contacts found for query: %s" query
        else
            printfn "Matching contacts:"
            matchingContacts |> List.iter (printfn "%s")

