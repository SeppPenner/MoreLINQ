﻿using System;
using System.Globalization;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using LinqEnumerable = System.Linq.Enumerable;

namespace MoreLinq.Test
{
    [TestFixture]
    public class ToDelimitedStringTest
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToDelimitedStringWithNullSequence()
        {
            Enumerable.ToDelimitedString<int>(null, ",");
        }

        [Test]
        public void ToDelimitedStringWithEmptySequence()
        {
            Assert.That(Enumerable.ToDelimitedString(LinqEnumerable.Empty<int>()), Is.Empty);
        }

        [Test]
        public void ToDelimitedStringWithNonEmptySequenceAndDelimiter()
        {
            var result = Enumerable.ToDelimitedString(new[] { 1, 2, 3 }, "-");
            Assert.That(result, Is.EqualTo("1-2-3"));
        }

        [Test]
        public void ToDelimitedStringWithNonEmptySequenceAndDefaultDelimiter()
        {
            using (new CurrentThreadCultureScope(new CultureInfo("fr-FR")))
            {
                var result = Enumerable.ToDelimitedString(new[] {1, 2, 3});
                Assert.That(result, Is.EqualTo("1;2;3"));
            }
        }

        [Test]
        public void ToDelimitedStringWithNonEmptySequenceAndNullDelimiter()
        {
            using (new CurrentThreadCultureScope(new CultureInfo("fr-FR")))
            {
                var result = Enumerable.ToDelimitedString(new[] { 1, 2, 3 }, null);
                Assert.That(result, Is.EqualTo("1;2;3"));
            }
        }

        [Test]
        public void ToDelimitedStringWithNonEmptySequenceContainingNulls()
        {
            var result = Enumerable.ToDelimitedString(new object[] { 1, null, "foo", true }, ",");
            Assert.That(result, Is.EqualTo("1,,foo,True"));
        }
    }
}