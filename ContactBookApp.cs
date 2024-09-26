using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class Contact
{
    public string Name { get; set; }
    public string Department { get; set; }
    public string Email { get; set; }

    public Contact(string name, string department, string email)
    {
        Name = name;
        Department = department;
        Email = email;
    }
}

class ContactBook
{
    private List<Contact> contacts;

    public ContactBook()
    {
        contacts = new List<Contact>();
        // Prepopulate contacts
        contacts.Add(new Contact("Sanchit Mathur", "IT", "sanchit.mathur@minosha.in"));
        contacts.Add(new Contact("Suraj Singh Negi", "MANAGER-IT", "suraj.negi@minosha.in"));
        contacts.Add(new Contact("Sunny Kansal", "HEAD-IT", "sunny.kansal@minosha.in"));
        contacts.Add(new Contact("Charanjeet Singh Kohli", "MANAGER - HR", "charanjeet.kohli@minosha.in"));
    }

    public void AddContact(Contact contact)
    {
        if (contacts.Exists(c => c.Name.Equals(contact.Name, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Contact with this name already exists.");
            return;
        }

        contacts.Add(contact);
        Console.WriteLine("Contact added successfully.");
    }

    public void SearchContact(string name)
    {
        foreach (Contact contact in contacts)
        {
            if (contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Contact found:");
                Console.WriteLine($"Name: {contact.Name}\nDepartment: {contact.Department}\nEmail: {contact.Email}");
                return;
            }
        }
        Console.WriteLine("Contact not found.");
    }

    public void UpdateContact(string name)
    {
        foreach (Contact contact in contacts)
        {
            if (contact.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                Console.WriteLine("Enter new department (leave blank to keep existing):");
                string newDepartment = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newDepartment))
                {
                    contact.Department = newDepartment;
                }

                Console.WriteLine("Enter new email (leave blank to keep existing):");
                string newEmail = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(newEmail) && IsValidEmail(newEmail))
                {
                    contact.Email = newEmail;
                }

                Console.WriteLine("Contact updated successfully.");
                return;
            }
        }
        Console.WriteLine("Contact not found.");
    }

    public void RemoveContact(string name)
    {
        Contact contactToRemove = contacts.Find(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (contactToRemove != null)
        {
            contacts.Remove(contactToRemove);
            Console.WriteLine("Contact removed successfully.");
        }
        else
        {
            Console.WriteLine("Contact not found.");
        }
    }

    public void ListContacts()
    {
        if (contacts.Count == 0)
        {
            Console.WriteLine("No contacts available.");
            return;
        }

        Console.WriteLine("Contacts:");
        foreach (Contact contact in contacts)
        {
            Console.WriteLine($"Name: {contact.Name}, Department: {contact.Department}, Email: {contact.Email}");
        }
    }

    private bool IsValidEmail(string email)
    {
        var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        return Regex.IsMatch(email, emailPattern);
    }
}

class Program
{
    static void Main(string[] args)
    {
        ContactBook contactBook = new ContactBook();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("\n1. Add a contact");
            Console.WriteLine("2. Search for a contact");
            Console.WriteLine("3. Update a contact");
            Console.WriteLine("4. Remove a contact");
            Console.WriteLine("5. List all contacts");
            Console.WriteLine("6. Exit");

            Console.Write("Enter your choice: ");
            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid choice. Try again.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter name: ");
                    string name = Console.ReadLine();
                    Console.Write("Enter department: ");
                    string department = Console.ReadLine();
                    Console.Write("Enter email: ");
                    string email = Console.ReadLine();

                    Contact contact = new Contact(name, department, email);
                    contactBook.AddContact(contact);
                    break;
                case 2:
                    Console.Write("Enter name to search: ");
                    string searchName = Console.ReadLine();
                    contactBook.SearchContact(searchName);
                    break;
                case 3:
                    Console.Write("Enter name to update: ");
                    string updateName = Console.ReadLine();
                    contactBook.UpdateContact(updateName);
                    break;
                case 4:
                    Console.Write("Enter name to remove: ");
                    string removeName = Console.ReadLine();
                    contactBook.RemoveContact(removeName);
                    break;
                case 5:
                    contactBook.ListContacts();
                    break;
                case 6:
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }
}
