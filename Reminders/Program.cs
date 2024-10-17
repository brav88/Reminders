using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Reminders
{
	internal class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Reminders");
			int opt = 0;
			string vopt = string.Empty;
			LinkedList linkedList = new LinkedList();


			do
			{
				Console.WriteLine("What do you want to do?");
				Console.WriteLine("1. Save a reminder");
				Console.WriteLine("2. See all the reminders");
				Console.WriteLine("9. Exit"); ;

				do
				{
					Console.WriteLine("Please choose an option with a number");
					vopt = Console.ReadLine();
				} while (!int.TryParse(vopt, out opt));

				if (opt == 1)
				{
					Console.WriteLine("Set the title:");
					string title = Console.ReadLine();

					Console.WriteLine("Set the priority:");
					int priority = Convert.ToInt16(Console.ReadLine());

					Console.WriteLine("Set the due date:");
					DateTime dueDate = Convert.ToDateTime(Console.ReadLine());

					linkedList.AddPositionByPriority2(title, priority, dueDate);
					Console.WriteLine("Reminder saved");
				}
				if (opt == 2)
				{
					linkedList.Print();
				}

			} while (opt != 9);
		}
	}

	internal class Node
	{
		public string Title;
		public int Priority;
		public DateTime DueDate;
		public Node Next;

		public Node(string _title, int _priority, DateTime _dueDate)
		{
			this.Title = _title;
			this.Priority = _priority;
			DueDate = _dueDate;
		}
	}

	internal class LinkedList
	{
		private Node Head;
		private int count = 0;

		public LinkedList()
		{
			Head = null;
		}

		public void Add(string _title, int _priority, DateTime _dueDate)
		{
			Node newNode = new Node(_title, _priority, _dueDate);

			if (Head == null)
			{
				Head = newNode;
			}
			else
			{
				Node node = Head;
				while (node.Next != null)
				{
					node = node.Next;
				}
				node.Next = newNode;
			}
			count++;
		}

		public void AddPosition(string _title, int _priority, DateTime _dueDate)
		{
			Node newNode = new Node(_title, _priority, _dueDate);
			Node pivot = Head;

			while (pivot != null)
			{
				if (pivot.Title == _title)
				{
					newNode.Next = pivot.Next;
					pivot.Next = newNode;
					return;
				}

				pivot = pivot.Next;
			}


			count++;
		}

		public void AddPositionByPriority(string _title, int _priority, DateTime _dueDate)
		{
			if (Head == null)
			{
				this.Add(_title, _priority, _dueDate);	
			}
			else
			{
				Node newNode = new Node(_title, _priority, _dueDate);
				Node pivot = Head;

				while (pivot != null)
				{
					if (pivot.Priority >= _priority)
					{												
						newNode.Next = pivot;

						if (pivot == Head)
						{
							Head = newNode;
						}
						
						return;
					}									
					if (pivot.Next != null)
					{
						if (pivot.Next.Priority >= _priority)
						{
							newNode.Next = pivot.Next;
							pivot.Next = newNode;

							return;
						}
					}
					else
					{
						this.Add(_title, _priority, _dueDate);
						return;
					}

					pivot = pivot.Next;
				}
			}

			count++;
		}

		public void AddPositionByPriority2(string _title, int _priority, DateTime _dueDate)
		{
			Node newNode = new Node(_title, _priority, _dueDate);

			// If the list is empty or the new node has higher priority than the head
			if (Head == null || Head.Priority >= _priority)
			{
				newNode.Next = Head;
				Head = newNode;
			}
			else
			{
				Node pivot = Head;

				// Traverse the list to find the correct insertion point
				while (pivot.Next != null && pivot.Next.Priority < _priority)
				{
					pivot = pivot.Next;
				}

				// Insert the new node in the correct position
				newNode.Next = pivot.Next;
				pivot.Next = newNode;
			}

			count++;
		}

		public void Print()
		{
			if (isEmpty())
			{
				Console.WriteLine("Linked List is empty");
				return;
			}

			Node node = Head;

			while (node != null)
			{
				Console.WriteLine("_____________________________________________________");
				Console.WriteLine("Priority "+ node.Priority + " | Title: " + node.Title + " | Due Date: " + node.DueDate);
				Console.WriteLine("_____________________________________________________");
				node = node.Next;
			}
		}

		public bool isEmpty()
		{
			return Head == null;
		}
	}
}
