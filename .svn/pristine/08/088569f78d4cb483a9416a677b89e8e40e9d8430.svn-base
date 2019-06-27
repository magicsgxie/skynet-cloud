using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UWay.Skynet.Cloud.Reflection;
using System.Reflection;


namespace UWay.Skynet.Cloud.Mapping.Internal
{
    sealed class CollectionMapper : MapperBase
    {
        private bool isFromColl;
        private bool isToColl;

        public CollectionMapper(Type fromType, Type toType)
            : base(fromType, toType)
        {
            isFromColl = fromType.IsCollectionTypeExcludeStringAndDictionaryType();// Types.IEnumerable.IsAssignableFrom(fromType);
            isToColl = toType.IsCollectionTypeExcludeStringAndDictionaryType();// Types.IEnumerable.IsAssignableFrom(toType);
        }

        public override void Map(ref object from, ref object to)
        {
            if (from == null)
                return;

            var newFrom = from;
            var fromType = _Info.From;

            if (isToColl)
            {

                if (!isFromColl)
                {
                    var array = Array.CreateInstance(fromType, 1);
                    array.SetValue(from, 0);
                    newFrom = array;

                    fromType = fromType.MakeArrayType();
                }

                if (_Info.To.HasElementType)
                    to = new ArrayConverter().Convert((IEnumerable)newFrom, fromType, _Info.To);
                else
                    to = new EnumarableConverter().Convert((IEnumerable)newFrom, fromType, _Info.To);
            }
            else
            {
                newFrom = (newFrom as IEnumerable).Cast<object>().FirstOrDefault();
                if (newFrom == null)
                    return;

                fromType = newFrom.GetType();

                to = Mapper.Map(newFrom, fromType, _Info.To);
            }
        }


        abstract class EnumarableConverterBase<TEnumerable> where TEnumerable : class, IEnumerable
        {
            public object Convert(IEnumerable soure, Type sourceType, Type targetType)
            {
                var soureElementType = TypeHelper.GetElementType(sourceType, soure);
                var targetElementType = TypeHelper.GetElementType(targetType);

                var source2 = soure.Cast<object>();
                var count = source2.Count();

                var target = CreateTarget(targetType, targetElementType, count);

                InternalConvert(soureElementType, targetElementType, source2, target);
                return target;

            }

            protected abstract void InternalConvert(Type soureElementType, Type targetElementType, IEnumerable<object> source, TEnumerable target);
            protected abstract TEnumerable CreateTarget(Type targetType, Type targetElementType, int length);
        }

        class ArrayConverter : EnumarableConverterBase<Array>
        {
            protected override void InternalConvert(Type soureElementType, Type targetElementType, IEnumerable<object> source, Array target)
            {
                int arrayIndex = 0;

                foreach (object sourceElementValue in source)
                {
                    object targetElementValue = null;
                    if (sourceElementValue != null)
                    {
                        soureElementType = sourceElementValue.GetType();

                        targetElementValue = targetElementType == soureElementType
                            || targetElementType.IsAssignableFrom(soureElementType)
                            ? sourceElementValue : Mapper.Map(sourceElementValue, soureElementType, targetElementType);
                    }
                    else
                    {
                        //TODO:default value
                    }


                    target.SetValue(targetElementValue, arrayIndex);
                    arrayIndex++;
                }

            }

            protected override Array CreateTarget(Type targetType, Type targetElementType, int length)
            {
                return Array.CreateInstance(targetElementType, length);
            }
        }

        class EnumarableConverter : EnumarableConverterBase<IList>
        {
            protected override void InternalConvert(Type soureElementType, Type targetElementType, IEnumerable<object> source, IList target)
            {
                foreach (object sourceElementValue in source)
                {
                    object targetElementValue = null;
                    if (sourceElementValue != null)
                    {
                        soureElementType = sourceElementValue.GetType();

                        targetElementValue = targetElementType == soureElementType
                            || targetElementType.IsAssignableFrom(soureElementType)
                            ? sourceElementValue : Mapper.Map(sourceElementValue, soureElementType, targetElementType);
                    }
                    else
                    {
                        //TODO:default value
                    }
                    target.Add(targetElementValue);
                }
            }
            protected override IList CreateTarget(Type targetType, Type targetElementType, int length)
            {
                return ObjectCreator.CreateList(targetType, targetElementType, length);
            }
        }
    }

}
