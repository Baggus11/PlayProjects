﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
namespace Common.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Append a single item of type T to a given list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> Append<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }
        /// <summary>
        /// Swap
        /// By index
        /// TODO: null checks!
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="indexA"></param>
        /// <param name="indexB"></param>
        /// <returns></returns>
        public static IList<T> Swap<T>(this IList<T> list, int indexA, int indexB)
        {
            T tmp = list[indexA];
            list[indexA] = list[indexB];
            list[indexB] = tmp;
            return list;
        }
        /// <summary>
        /// Convert a List of a class type to Datatable of same type-schema
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDatatable<T>(this List<T> items)
        {
            DataTable dt = new DataTable();
            try
            {
                if (items.Count != 0)
                {
                    //Add headers:
                    var properties = items.First().GetType()
                         .GetProperties().ToList();
                    properties.ForEach(p => dt.Columns.Add(p.Name, p.PropertyType));
                    //Add all the values as new DataRows:
                    object[] values = new object[properties.Count];
                    foreach (T item in items)
                    {
                        for (int i = 0; i < values.Length; i++)
                        {
                            values[i] = properties[i].GetValue(item);
                        }
                        dt.Rows.Add(values);
                    }
                }
                else
                {
                    Debug.WriteLine("No line items found!");
                }
            }
            catch (Exception ex)
            {
                string errMsg = string.Format("{0}: {1}", MethodBase.GetCurrentMethod().Name, ex.ToString());
                Debug.WriteLine(errMsg);
            }
            return dt;
        }
    }
}