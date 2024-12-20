﻿#region License and information
/* * * * *
 * 
 * Extension file for the SimpleJSON framework for better support of some common
 * .NET types. It does only work together with the SimpleJSON.cs
 * It provides direct conversion support for types like decimal, char, byte,
 * sbyte, short, ushort, uint, DateTime, TimeSpan and Guid. In addition there
 * are conversion helpers for converting an array of number values into a byte[]
 * or a List<byte> as well as converting an array of string values into a string[]
 * or List<string>.
 * Finally there are some additional type conversion operators for some nullable
 * types like short?, int?, float?, double?, long? and bool?. They will actually
 * assign a JSONNull value when it's null or a JSONNumber when it's not.
 * 
 * The MIT License (MIT)
 * 
 * Copyright (c) 2020 Markus Göbel (Bunny83)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 * 
 * * * * */

#endregion License and information

using System.Collections.Generic;
using System.Globalization;

namespace DTT.DailyRewards
{
    internal partial class JsonNode
    {
        #region Decimal
        public virtual decimal AsDecimal
        {
            get
            {
                decimal result;
                if (!decimal.TryParse(Value, out result))
                    result = 0;
                return result;
            }
            set
            {
                Value = value.ToString();
            }
        }

        public static implicit operator JsonNode(decimal aDecimal)
        {
            return new JsonString(aDecimal.ToString());
        }

        public static implicit operator decimal(JsonNode aNode)
        {
            return aNode.AsDecimal;
        }
        #endregion Decimal

        #region Char
        public virtual char AsChar
        {
            get
            {
                if (IsString && Value.Length > 0)
                    return Value[0];
                if (IsNumber)
                    return (char)AsInt;
                return '\0';
            }
            set
            {
                if (IsString)
                    Value = value.ToString();
                else if (IsNumber)
                    AsInt = (int)value;
            }
        }

        public static implicit operator JsonNode(char aChar)
        {
            return new JsonString(aChar.ToString());
        }

        public static implicit operator char(JsonNode aNode)
        {
            return aNode.AsChar;
        }
        #endregion Decimal

        #region UInt
        public virtual uint AsUInt
        {
            get
            {
                return (uint)AsDouble;
            }
            set
            {
                AsDouble = value;
            }
        }

        public static implicit operator JsonNode(uint aUInt)
        {
            return new JsonNumber(aUInt);
        }

        public static implicit operator uint(JsonNode aNode)
        {
            return aNode.AsUInt;
        }
        #endregion UInt

        #region Byte
        public virtual byte AsByte
        {
            get
            {
                return (byte)AsInt;
            }
            set
            {
                AsInt = value;
            }
        }

        public static implicit operator JsonNode(byte aByte)
        {
            return new JsonNumber(aByte);
        }

        public static implicit operator byte(JsonNode aNode)
        {
            return aNode.AsByte;
        }
        #endregion Byte
        #region SByte
        public virtual sbyte AsSByte
        {
            get
            {
                return (sbyte)AsInt;
            }
            set
            {
                AsInt = value;
            }
        }

        public static implicit operator JsonNode(sbyte aSByte)
        {
            return new JsonNumber(aSByte);
        }

        public static implicit operator sbyte(JsonNode aNode)
        {
            return aNode.AsSByte;
        }
        #endregion SByte

        #region Short
        public virtual short AsShort
        {
            get
            {
                return (short)AsInt;
            }
            set
            {
                AsInt = value;
            }
        }

        public static implicit operator JsonNode(short aShort)
        {
            return new JsonNumber(aShort);
        }

        public static implicit operator short(JsonNode aNode)
        {
            return aNode.AsShort;
        }
        #endregion Short
        #region UShort
        public virtual ushort AsUShort
        {
            get
            {
                return (ushort)AsInt;
            }
            set
            {
                AsInt = value;
            }
        }

        public static implicit operator JsonNode(ushort aUShort)
        {
            return new JsonNumber(aUShort);
        }

        public static implicit operator ushort(JsonNode aNode)
        {
            return aNode.AsUShort;
        }
        #endregion UShort

        #region DateTime
        public virtual System.DateTime AsDateTime
        {
            get
            {
                System.DateTime result;
                if (!System.DateTime.TryParse(Value, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    result = new System.DateTime(0);
                return result;
            }
            set
            {
                Value = value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public static implicit operator JsonNode(System.DateTime aDateTime)
        {
            return new JsonString(aDateTime.ToString(CultureInfo.InvariantCulture));
        }

        public static implicit operator System.DateTime(JsonNode aNode)
        {
            return aNode.AsDateTime;
        }
        #endregion DateTime
        #region TimeSpan
        public virtual System.TimeSpan AsTimeSpan
        {
            get
            {
                System.TimeSpan result;
                if (!System.TimeSpan.TryParse(Value, CultureInfo.InvariantCulture, out result))
                    result = new System.TimeSpan(0);
                return result;
            }
            set
            {
                Value = value.ToString();
            }
        }

        public static implicit operator JsonNode(System.TimeSpan aTimeSpan)
        {
            return new JsonString(aTimeSpan.ToString());
        }

        public static implicit operator System.TimeSpan(JsonNode aNode)
        {
            return aNode.AsTimeSpan;
        }
        #endregion TimeSpan

        #region Guid
        public virtual System.Guid AsGuid
        {
            get
            {
                System.Guid result;
                System.Guid.TryParse(Value, out result);
                return result;
            }
            set
            {
                Value = value.ToString();
            }
        }

        public static implicit operator JsonNode(System.Guid aGuid)
        {
            return new JsonString(aGuid.ToString());
        }

        public static implicit operator System.Guid(JsonNode aNode)
        {
            return aNode.AsGuid;
        }
        #endregion Guid

        #region ByteArray
        public virtual byte[] AsByteArray
        {
            get
            {
                if (this.IsNull || !this.IsArray)
                    return null;
                int count = Count;
                byte[] result = new byte[count];
                for (int i = 0; i < count; i++)
                    result[i] = this[i].AsByte;
                return result;
            }
            set
            {
                if (!IsArray || value == null)
                    return;
                Clear();
                for (int i = 0; i < value.Length; i++)
                    Add(value[i]);
            }
        }

        public static implicit operator JsonNode(byte[] aByteArray)
        {
            return new JsonArray { AsByteArray = aByteArray };
        }

        public static implicit operator byte[](JsonNode aNode)
        {
            return aNode.AsByteArray;
        }
        #endregion ByteArray
        #region ByteList
        public virtual List<byte> AsByteList
        {
            get
            {
                if (this.IsNull || !this.IsArray)
                    return null;
                int count = Count;
                List<byte> result = new List<byte>(count);
                for (int i = 0; i < count; i++)
                    result.Add(this[i].AsByte);
                return result;
            }
            set
            {
                if (!IsArray || value == null)
                    return;
                Clear();
                for (int i = 0; i < value.Count; i++)
                    Add(value[i]);
            }
        }

        public static implicit operator JsonNode(List<byte> aByteList)
        {
            return new JsonArray { AsByteList = aByteList };
        }

        public static implicit operator List<byte> (JsonNode aNode)
        {
            return aNode.AsByteList;
        }
        #endregion ByteList

        #region StringArray
        public virtual string[] AsStringArray
        {
            get
            {
                if (this.IsNull || !this.IsArray)
                    return null;
                int count = Count;
                string[] result = new string[count];
                for (int i = 0; i < count; i++)
                    result[i] = this[i].Value;
                return result;
            }
            set
            {
                if (!IsArray || value == null)
                    return;
                Clear();
                for (int i = 0; i < value.Length; i++)
                    Add(value[i]);
            }
        }

        public static implicit operator JsonNode(string[] aStringArray)
        {
            return new JsonArray { AsStringArray = aStringArray };
        }

        public static implicit operator string[] (JsonNode aNode)
        {
            return aNode.AsStringArray;
        }
        #endregion StringArray
        #region StringList
        public virtual List<string> AsStringList
        {
            get
            {
                if (this.IsNull || !this.IsArray)
                    return null;
                int count = Count;
                List<string> result = new List<string>(count);
                for (int i = 0; i < count; i++)
                    result.Add(this[i].Value);
                return result;
            }
            set
            {
                if (!IsArray || value == null)
                    return;
                Clear();
                for (int i = 0; i < value.Count; i++)
                    Add(value[i]);
            }
        }

        public static implicit operator JsonNode(List<string> aStringList)
        {
            return new JsonArray { AsStringList = aStringList };
        }

        public static implicit operator List<string> (JsonNode aNode)
        {
            return aNode.AsStringList;
        }
        #endregion StringList

        #region NullableTypes
        public static implicit operator JsonNode(int? aValue)
        {
            if (aValue == null)
                return JsonNull.CreateOrGet();
            return new JsonNumber((int)aValue);
        }
        public static implicit operator int?(JsonNode aNode)
        {
            if (aNode == null || aNode.IsNull)
                return null;
            return aNode.AsInt;
        }

        public static implicit operator JsonNode(float? aValue)
        {
            if (aValue == null)
                return JsonNull.CreateOrGet();
            return new JsonNumber((float)aValue);
        }
        public static implicit operator float? (JsonNode aNode)
        {
            if (aNode == null || aNode.IsNull)
                return null;
            return aNode.AsFloat;
        }

        public static implicit operator JsonNode(double? aValue)
        {
            if (aValue == null)
                return JsonNull.CreateOrGet();
            return new JsonNumber((double)aValue);
        }
        public static implicit operator double? (JsonNode aNode)
        {
            if (aNode == null || aNode.IsNull)
                return null;
            return aNode.AsDouble;
        }

        public static implicit operator JsonNode(bool? aValue)
        {
            if (aValue == null)
                return JsonNull.CreateOrGet();
            return new JsonBool((bool)aValue);
        }
        public static implicit operator bool? (JsonNode aNode)
        {
            if (aNode == null || aNode.IsNull)
                return null;
            return aNode.AsBool;
        }

        public static implicit operator JsonNode(long? aValue)
        {
            if (aValue == null)
                return JsonNull.CreateOrGet();
            return new JsonNumber((long)aValue);
        }
        public static implicit operator long? (JsonNode aNode)
        {
            if (aNode == null || aNode.IsNull)
                return null;
            return aNode.AsLong;
        }

        public static implicit operator JsonNode(short? aValue)
        {
            if (aValue == null)
                return JsonNull.CreateOrGet();
            return new JsonNumber((short)aValue);
        }
        public static implicit operator short? (JsonNode aNode)
        {
            if (aNode == null || aNode.IsNull)
                return null;
            return aNode.AsShort;
        }
        #endregion NullableTypes
    }
}
