namespace SlayTheHydra
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class Hydra<T>
    {
        public Head<T> Body { get; set; }
        public long HeadCount { get; set; }

        public Hydra()
        {
            Body = new Head<T>(default(T), null);
            HeadCount = 0;
        }

        /// <summary>
        /// Adds a head to the hydra
        /// </summary>
        /// <param name="value">head value</param>
        /// <param name="subHeadsListing">indication of heads' indexes to which to add the head, starting with the heads attached directly to the body</param>
        public void AddHead(T value, params int[] subHeadsListing)
        {
            Head<T> currentHead = this.Body;

            foreach (int headNumber in subHeadsListing)
            {
                currentHead = currentHead.SubHeads[headNumber];
            }

            Head<T> newHead = new Head<T>(value, currentHead);
            currentHead.SubHeads.Add(newHead);
            HeadCount++;
        }

        /// <summary>
        /// Chops a hydra terminal head, returns a console message if the head is not terminal
        /// </summary>
        /// <param name="subHeadsListing">parameters specifying the head to chop - each int indicates head index and moves to that head</param>
        public void ChopHead(params int[] subHeadsListing)
        {
            Head<T> currentHead = this.Body;
            foreach (int headNumber in subHeadsListing)
            {
                currentHead = currentHead.SubHeads[headNumber];
            }

            if (currentHead.SubHeads.Count > 0)
            {
                List<T> currentHeadLineage = new List<T>();
                currentHeadLineage.Add(currentHead.Value);

                while (currentHead.Parent != null)
                {
                    currentHeadLineage.Add(currentHead.Parent.Value);
                    currentHead = currentHead.Parent;
                }

                currentHeadLineage.Reverse();
                currentHeadLineage.RemoveAt(0);

                const string joinerBase = "|";
                string joiner = string.Empty;

                StringBuilder sb = new StringBuilder();
                sb.Append("body-> ");

                foreach (T value in currentHeadLineage)
                {
                    joiner = string.Concat(joiner, joinerBase);
                    sb.Append($"{string.Concat(value, " ", joiner, " ")}");
                }

                string output = sb.ToString();
                output = output.TrimEnd(' ');
                output = output.TrimEnd('|');
                Console.Write(output);

                Console.WriteLine(" <-This head cannot be chopped, because it's not terminal");
                return;
            }

            currentHead = currentHead.Parent;

            // The last parameter specifies the index of the head to be deleted in its parent child heads
            currentHead.SubHeads.RemoveAt(subHeadsListing.Last());
            this.HeadCount--;

            GrowHeads(currentHead);
        }

        public void GrowHeads(Head<T> currentHead)
        {
            // Heads attached directly to the body are choped without inducing growth of new heads (the hydra must be defeated at some point, after all)
            if (currentHead.Parent == null)
            {
                return;
            }

            Queue<Head<T>> growQueue = new Queue<Head<T>>();
            Queue<Head<T>> growingHeadsQueue = new Queue<Head<T>>();

            growQueue.Enqueue(currentHead);
            growingHeadsQueue.Enqueue(currentHead);

            while (growQueue.Count > 0)
            {
                Head<T> queueCurrentHead = growQueue.Dequeue();

                foreach (Head<T> head in queueCurrentHead.SubHeads)
                {
                    growQueue.Enqueue(head);
                    growingHeadsQueue.Enqueue(head);
                }
            }


        }

        /// <summary>
        /// Draws to the console the whole hydra, by calling a recursive private method
        /// </summary>
        public void DrawHydra()
        {
            const string leftPadding = " ";
            DrawHydra(this.Body, leftPadding);
            Console.WriteLine($"{Environment.NewLine}Heads:{this.HeadCount}");
        }

        /// <summary>
        /// Private recursive method for drawing to the console the whole hydra
        /// </summary>
        /// <param name="currentHead">head to start the recursion from</param>
        /// <param name="depth">string indicator for each level after the first</param>
        private void DrawHydra(Head<T> currentHead, string depth)
        {
            foreach (Head<T> head in currentHead.SubHeads)
            {
                string levelIndicator = "|";
                Console.WriteLine(depth + "   " + head.Value);
                DrawHydra(head, string.Concat(depth, levelIndicator));
            }
        }

        public class Head<T>
        {
            //let's assign a value to heads for a better visualization (and maybe more ideas in the future)
            public T Value { get; set; }

            public List<Head<T>> SubHeads { get; set; }
            public Head<T> Parent { get; set; }

            public Head(T value, Head<T> parent)
            {
                this.Value = value;
                this.SubHeads = new List<Head<T>>();
                this.Parent = parent;
            }
        }
    }
}