// Copyright: Robert P Herbig, 2011

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace EnterpriseyAwesomeness
{
    // This solution is intended as satire. Any resemblance to real code, living or dead, is purely coincidental.
    // Let me know if you have a suggestion on making this more Enterprise-ready.

    public class Program
    {
        private static void Main()
        {
            Test(0, new DateTime(2010, 1, 1), new DateTime(2010, 1, 2));
            Test(1, new DateTime(2012, 1, 1), new DateTime(2012, 1, 15));
            Test(2, new DateTime(2011, 5, 1), new DateTime(2012, 1, 15));
            Test(5, new DateTime(2010, 1, 1), new DateTime(2012, 12, 30));
            Console.ReadKey();
        }

        private static void Test(int expected, DateTime start, DateTime end)
        {
            // This should prove that the code is Enterprisey enough for immediate deployment.
            int actual = new Counter<DateTime>(
                new List<IFilter<DateTime>>
                    {
                        new Filter<DateTime, DayOfWeek>(
                            DayOfWeek.Friday,
                            Comparer<DayOfWeek>.Default,
                            dt_input => dt_input.DayOfWeek),
                        new Filter<DateTime, int>(
                            13,
                            Comparer<int>.Default,
                            dt_input => dt_input.Day),
                    }
                ).Filter(
                    start,
                    end,
                    Comparer<DateTime>.Default,
                    dt_input => dt_input.AddDays(1));

            Console.WriteLine(String.Format("{0} on {1}", (expected == actual) ? "Succeeded" : "Failed", expected));
        }
    }

    // Interfaces are a must, of course
    public interface IFilterer<T>
    {
        // You may notice the prefix for each var name. It's essential, otherwise
        // how would you remember that "start" is of type "T"? But since T changes,
        // we'd better go with the "o" prefix (for object) so we're never wrong.
        int Filter(T o_start, T o_end, Comparer<T> co_comparer, Func<T, T> foo_incrementor);
    }
    
    public interface IFilter<in T>
    {
        bool Matches(T o_input);
    }

    public class Counter<T> : IFilterer<T>
    {
        // Here there is a prefix to the prefix, so that you can easily
        // remember that "iifo_filters" is private and readonly.
        private readonly IEnumerable<IFilter<T>> pr_iifo_filters;

        // Originally I used params, but who knows what wild sorts of
        // things could occur? Best to use another layer of generics.
        public Counter(IEnumerable<IFilter<T>> iifo_filters)
        {
            // Don't take a chance that someone could cast the IEnumerable to a List and modify it.
            pr_iifo_filters = new ReadOnlyCollection<IFilter<T>>(iifo_filters.ToList());
        }

        public int Filter(T o_start, T o_end, Comparer<T> co_comparer, Func<T, T> foo_incrementor)
        {
            int i_count = 0;
            for (T o_current = o_start; co_comparer.Compare(o_current, o_end) < 0; o_current = foo_incrementor(o_current))
            {
                T o_temp = o_current;
                bool b_matches = pr_iifo_filters.Aggregate(true,
                                                             (o_input, ifo_filter) =>
                                                             o_input && ifo_filter.Matches(o_temp));

                if (b_matches)
                {
                    i_count++;
                }
            }

            return i_count;
        }
    }

    // This allows any two data types to be compared for maximum flexibility,
    // reusability, extensibility, adaptability, responsibility, configurability,
    // maintainability, supportability, serviceability, stability, modularity,
    // parameterization, and orthogonality.
    public class Filter<T, U> : IFilter<T>
    {
        private readonly U pr_o_target;
        private readonly Comparer<U> pr_co_comparer;
        private readonly Func<T, U> pr_foo_extractor;

        // It's critical to allow substitution of a different Comparer, if needed; and
        // to allow the caller to perform any processing on the input, if necessary.
        public Filter(U o_target, Comparer<U> co_comparer, Func<T, U> foo_extractor)
        {
            pr_o_target = o_target;
            pr_co_comparer = co_comparer;
            pr_foo_extractor = foo_extractor;
        }

        public bool Matches(T o_input)
        {
            return pr_co_comparer.Compare(pr_foo_extractor(o_input), pr_o_target) == 0;
        }
    }
}
