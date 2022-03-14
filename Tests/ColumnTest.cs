using System;
using TableMaker;
using NUnit.Framework;

namespace Tests
{
    public class ColumnTest
    {
        private IColumn _column;

        [SetUp]
        public void SetUp()
        {
            _column = new Column("test");
            _column.AddValue( 1 );
        }

        [Test]
        public void Ctor_EmptyColumnName_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => new Column( "" ) );
            Assert.Throws<ArgumentException>( () => new Column( null ) );
        }

        [Test]
        public void Ctor_ValidName_NoExceptions()
        {
            Assert.DoesNotThrow( () => new Column( "Test" ) );
        }

        [Test]
        public void AddValue_DoubleValue_NoExceptions()
        {
            Assert.DoesNotThrow( () => _column.AddValue( -100.2 ) );
        }

        [Test]
        public void AddValue_FloatValue_NoExceptions()
        {
            Assert.DoesNotThrow( () => _column.AddValue( 10.3f ) );
        }

        [Test]
        public void SetValue_UnexistingIndex_ArgumentException()
        {
           Assert.Throws<ArgumentException>( () => _column.SetValue(10.0, _column.Count));
           Assert.Throws<ArgumentException>( () => _column.SetValue(10.0, -1));
        }

        [Test]
        public void SetValue_FloatValue_NoExceptions()
        {
            Assert.DoesNotThrow( () => _column.SetValue( 10.2f, 0 ) );
        }

        [Test]
        public void SetValue_DoubleValue_NoExceptions()
        {
            Assert.DoesNotThrow( () => _column.SetValue( 10.2, 0 ) );
        }

        [Test]
        public void GetValue_ValidIndex_NoExceptions()
        {
            // Arrange
            var value = 100.4;

            // Act
            _column.AddValue( value );
            var result = _column.GetValue( 1 );

            // Assert
            Assert.AreEqual( result, value );
        }

        [Test]
        public void GetValue_InvalidIndex_ArgumentException()
        {
            Assert.DoesNotThrow( () => _column.GetValue( _column.Count - 1 ) );
        }

        [Test]
        public void WidthUpdatingWithoutRound()
        {
            // Arrange
            var value = "1233333333";
            var expectedWidth = value.Length;

            // Act
            _column.AddValue( Double.Parse( value ) );
            var lastValue = _column.GetValue( _column.Count - 1 );
            var currentWidth = lastValue.ToString().Length;

            // Assert
            Assert.AreEqual( expectedWidth, currentWidth );
        }

        [Test]
        public void PrintName_TestWidth()
        {
            // Arrange 
            var value = 1233333;
            var expectedWidth = value.ToString().Length;
            _column.AddValue( value );

            // Act 
            var result = _column.PrintName();
            var currentWidth = result.Length;

            // Assert
            Assert.AreEqual( expectedWidth, currentWidth );
        }

        [Test]
        public void PrintValue_TestWidth()
        {
            // Arrange 
            var value = 1233333;
            var expectedWidth = value.ToString().Length;
            _column.AddValue( value );

            // Act 
            var result = _column.PrintValue( _column.Count - 1 );
            var currentWidth = result.Length;

            // Assert
            Assert.AreEqual( expectedWidth, currentWidth );
        }

    }
}