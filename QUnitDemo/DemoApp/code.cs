﻿using System;
using Bridge;
using Bridge.Html5;

namespace DemoApp
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class PersonService
    {
        public Person GetPerson(int personId)
        {
            return new Person
            {
                Id = personId,
                Name = "Frank"
            };
        }
    }

    public class PersonView
    {
        public Element Root { get; private set; }

        public PersonView(Element root)
        {
            this.Root = root;
        }

        public LabelElement CreateLabelElement(string id, string title, string margin, bool isBold, HTMLColor color)
        {
            return new LabelElement()
            {
                Id = id,
                InnerHTML = title,
                Style =
                {
                    Margin = margin ?? string.Empty,
                    FontWeight = isBold ? "bold" : string.Empty,
                    Color = color
                }
            };
        }

        public void CreatePersonUIElements(Person person)
        {
            this.Root.AppendChild(CreateLabelElement(null, "Person:", "5px", true, HTMLColor.Black));

            this.Root.AppendChild(CreateLabelElement("lblPersonName", person.Name, null, false, HTMLColor.Blue));
        }
    }

    public class PersonApplication
    {
        public PersonService Service { get; private set; }
        public PersonView View { get; private set; }

        public PersonApplication(Element root)
        {
            this.Service = new PersonService();
            this.View = new PersonView(root);
        }

        public void RenderPerson()
        {
            var person = Service.GetPerson(55);

            View.CreatePersonUIElements(person);
        }
    }
}

