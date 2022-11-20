using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1
{
    public class UserSet
    {
        protected List<int> elementsValue = new List<int>();

        public List<int> elements
        {
            get
            {
                elementsValue.Sort();
                return elementsValue;
            }
            set
            {
                elementsValue = value;
            }
        }

        public UserSet()
        {
            elements = new List<int>();
        }

        public UserSet(in int[] elems)
        {
            foreach (int i in elems)
            {
                Add(i);
            }
        }

        public UserSet(in UserSet toCopy)
        {
            elements = toCopy.elements;
        }

        public void Add(in int elem)
        {
            if (!elements.Contains(elem))
            {
                elements.Add(elem);
            }
        }

        public void Add(in UserSet set)
        {
            foreach (int i in set.elements)
            {
                Add(i);
            }
        }

        public void Add(in int[] elems)
        {
            foreach (int i in elems)
            {
                Add(i);
            }
        }

        public void Add(in List<int> elems)
        {
            foreach (int i in elems)
            {
                Add(i);
            }
        }

        public void Clear()
        {
            elements.Clear();
        }

        public int Size()
        {
            return elements.Count();
        }        

        override public string ToString()
        {
            StringBuilder ans = new StringBuilder();
            ans.Append("{ ");
            if (Size() > 0)
            {
                foreach (int i in elements)
                {
                    ans.Append(i + " ");
                }
            }
            else
            {
                ans.Append("- ");
            }
            ans.Append("}");
            return ans.ToString();
        }

        public void Print()
        {
            Console.Write(this.ToString());
        }

        public bool Contains(in int elem)
        {
            return elements.Contains(elem);
        }

        public int Rand()
        {
            if (Size() != 0)
            {
                var rand = new Random();
                return elements[rand.Next() % Size()];
            }
            return 0;
        }

        public void IntersectWith(in UserSet set)
        {
            List<int> ans = new List<int>();
            foreach (int i in elements)
            {
                if (set.Contains(i))
                {
                    ans.Add(i);
                }
            }
            elements = ans;
        }

        public void UnionWith(in UserSet set)
        {
            foreach (int i in set.elements)
            {
                Add(i);
            }
        }

        public void ExceptWith(in UserSet set)
        {
            List<int> ans = new List<int>();
            foreach (int i in elements)
            {
                if (!set.Contains(i))
                {
                    ans.Append(i);
                }
            }
            elements = ans;
        }

        public void SymmetricExceptWith(in UserSet set)
        {
            var setCopy = new UserSet(set);
            var thisCopy = new UserSet(this);
            thisCopy.ExceptWith(set);
            setCopy.ExceptWith(this);
            elements = thisCopy.elements;
            Add(setCopy);
        }
    }
}
