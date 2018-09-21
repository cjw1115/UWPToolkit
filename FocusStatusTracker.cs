using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace UWPToolkit
{
    public class FocusStatusTracker
    {
        public static void InjectFocusAnalysis(Page page)
        {
            var _uielements = FindVisualChild<UIElement>(page);
            foreach (var element in _uielements)
            {
                element.GettingFocus += Element_GettingFocus;
                element.GotFocus += Element_GotFocus;
                element.LosingFocus += Element_LosingFocus;
                element.LostFocus += Element_LostFocus;
            }
        }

        private static void Element_GettingFocus(UIElement sender, Windows.UI.Xaml.Input.GettingFocusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"Getting Focus: {sender.GetType().Name}");
            args.Handled = true;
        }

        private static void Element_GotFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Got Focus: {sender.GetType().Name}"); 
        }

        private static void Element_LosingFocus(UIElement sender, Windows.UI.Xaml.Input.LosingFocusEventArgs args)
        {
            System.Diagnostics.Debug.WriteLine($"Losing Focus: {sender.GetType().Name}"); args.Handled = true;
        }

        private static void Element_LostFocus(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"Lost Focus: {sender.GetType().Name}"); 
        }

        public static T[] FindVisualChild<T>(Windows.UI.Xaml.DependencyObject obj) where T : Windows.UI.Xaml.UIElement
        {
            List<T> taragetElements = new List<T>();
            Queue<Windows.UI.Xaml.DependencyObject> queue = new Queue<Windows.UI.Xaml.DependencyObject>();
            queue.Enqueue(obj);
            while (queue.Count > 0)
            {
                var item = queue.Dequeue();

                int count = Windows.UI.Xaml.Media.VisualTreeHelper.GetChildrenCount(item);
                for (int i = 0; i < count; i++)
                {
                    var re = Windows.UI.Xaml.Media.VisualTreeHelper.GetChild(item, i);
                    var child = re as T;
                    if (child != null)
                    {
                        taragetElements.Add(child);
                    }
                    queue.Enqueue(child);
                }
            }
            return taragetElements.ToArray();
        }
    }
}
