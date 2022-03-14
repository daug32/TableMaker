using System;
using TableMaker;
using NUnit.Framework;
using System.Collections.Generic;

namespace Tests
{
    public class TableTest
    { 
        private ITable _table;

        [SetUp]
        public void SetUp()
        {
            _table = new Table();
            _table.AddColumns( new string[] { "Test1", "Test2" } );
        }

        [Test]
        public void AddColumn_EmptyOrNullName_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.AddColumn( "" ) );
            Assert.Throws<ArgumentException>( () => _table.AddColumn( null ) );
        }

        [Test]
        public void AddColumn_DuplicateColumns_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.AddColumn( "Test1" ) );
        }

        [Test]
        public void AddColumn_ValidName_NoExceptions()
        {
            Assert.DoesNotThrow( () => _table.AddColumn( "Another column" ) );
        }

        [Test]
        public void AddColumns_EmptyOrNullNames_ArgumentException()
        {
            var columnNames = new string[] { "", "Something" };
            Assert.Throws<ArgumentException>( () => _table.AddColumns( columnNames ) );

            columnNames = new string[] { null };
            Assert.Throws<ArgumentException>( () => _table.AddColumns( columnNames ) );
        }

        [Test]
        public void AddColumns_ValidArray_NoExceptions()
        {
            var columnNames = new string[] { "Test3", "Test number 4" };
            Assert.DoesNotThrow( () => _table.AddColumns( columnNames ) );
        }

        [Test]
        public void CheckColumn_EmptyOrNullName_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.ColumnExists( null ) );
            Assert.Throws<ArgumentException>( () => _table.ColumnExists( "" ) );
        }

        [Test]
        public void CheckColumn_ExistingColumn_True()
        {
            Assert.IsTrue( _table.ColumnExists( "Test1" ) );
        }

        [Test]
        public void CheckColumn_UnexistingColumn_False()
        {
            Assert.IsFalse( _table.ColumnExists( "NoColumn" ) );
        }

        [Test]
        public void AddValue_UnexistingColumn_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.AddValue( "NoColumn", 123 ) );
        }

        [Test]
        public void AddValue_ValidColumn_NoExceptions()
        {
            Assert.DoesNotThrow( () => _table.AddValue( "Test1", 123 ) );
        }

        [Test]
        public void SetValue_DoubleValue_InvalidIndex_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.SetValue( "Test1", 0, 123 ) );
        }

        [Test]
        public void SetValue_DoubleValue_UnexistingColumn_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.SetValue( "NoColumn", 0, 123 ) );
        }

        [Test]
        public void SetValue_DoubleValue_ValidIndexAndColumn_GetValueFReturnsRightValue()
        {
            // Arrange
            _table.AddValue( "Test1", 10);
            var expectedValue = 123.23;

            // Act
            _table.SetValue( "Test1", 0, expectedValue );
            var currentValue = _table.GetValue( "Test1", 0 );

            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void SetValue_FloatValue_ValidIndexAndColumn_GetValueFReturnsRightValue()
        {
            // Arrange
            _table.AddValue( "Test1", 10 );
            var expectedValue = 123.23f;

            // Act
            _table.SetValue( "Test1", 0, expectedValue );
            var currentValue = _table.GetValueF( "Test1", 0 );

            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void GetValue_UnexistingColumn_ArgumentException()
        {
            _table.AddValue( "Test1", 10 );
            Assert.Throws<ArgumentException>( () => _table.GetValue( "NoColumn", _table.RowCount - 1 ) );
        }

        [Test]
        public void GetValue_InvalidIndex_ArgumentException()
        {
            _table.AddValue( "Test1", 10 );
            Assert.Throws<ArgumentException>( () => _table.GetValue( "Test1", _table.RowCount ) );
            Assert.Throws<ArgumentException>( () => _table.GetValue( "Test1", -1 ) );
        }

        [Test]
        public void GetValue_ValidColumnAndIndex_RightValue()
        {
            // Arrange 
            var value = 10.23;
            _table.AddValue( "Test1", value );

            // Act
            var currentValue = _table.GetValue( "Test1", _table.RowCount - 1 );
            
            // Assert
            Assert.AreEqual( value, currentValue );
        }

        [Test]
        public void GetValueF_ValidColumnAndIndex_RightValue()
        {
            // Arrange 
            var value = 10.23f;
            _table.AddValue( "Test1", value );

            // Act
            var currentValue = _table.GetValueF( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( value, currentValue );
        }
        
        [Test]
        public void PrintValue_InvalidIndex_ArgumentExcpetion()
        {
            Assert.Throws<ArgumentException>( () => _table.PrintValue( "Test1", -1 ) );
            Assert.Throws<ArgumentException>( () => _table.PrintValue( "Test1", _table.RowCount ) );
        }

        [Test]
        public void PrintValue_UnexistingColumn_ArgumentExcpetion()
        {
            _table.AddValue( "Test1", 1.2222 );
            Assert.Throws<ArgumentException>( () => _table.PrintValue( "NoColumn", _table.RowCount - 1 ) );
        }

        [Test]
        public void PrintValue_FunctionalTest()
        {
            // Arrange
            var value = 1.2225652;
            _table.AddValue( "Test1", value );
            var expectedPritn = _table.Prefix + value + _table.Postfix;

            // Act
            var currentPrint = _table.PrintValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedPritn, currentPrint );
        }

        [ Test]
        public void AddRow_DoubleValues_ValuesCountMoreThanColumnsCount_ArgumentException()
        {
            var values = new double[] { 1, 23.3, 1 / 3 };
            Assert.Throws<ArgumentException>( () => _table.AddRow( values ) );
        }

        [Test]
        public void AddRow_DoubleValues_ValuesCountLessThanColumnsCount_NoExceptions()
        {
            var values = new double[] { 1 };
            Assert.DoesNotThrow( () => _table.AddRow( values ) );
        }

        [Test]
        public void AddRow_FloatValues_ValuesCountLessThanColumnsCount_NoExceptions()
        {
            var values = new float[] { 1.0f, 23.3f };
            Assert.DoesNotThrow( () => _table.AddRow( values ) );
        }

        [Test]
        public void AddRow_NullRow_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.AddRow( ( double[] )null ) );
        }

        [Test]
        public void GetRow_InvalidIndex_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.GetRow( -1 ) );
            Assert.Throws<ArgumentException>( () => _table.GetRow( _table.RowCount ) );
        }

        [Test]
        public void GetRow_ValidIndexAndNotAllColumnHaveValues_DoubleArrayWithElementsCountEqualToColumnsCount()
        {
            // Arrange
            var insertArray = new double[] { 1.2 };
            _table.AddRow( insertArray );
            var expectedArray = new double[] { 1.2, 0 };

            // Act
            var currentArray = _table.GetRow( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void GetRow_AddValueForOneColumn_GetDoubleArrayWithValuesForAllColumns()
        {
            // Arrange
            _table.AddValue( "Test1", 123 );
            var expectedArray = new double[] { 123, 0 };

            // Act
            var currentArray = _table.GetRow( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void GetRowF_InvalidIndex_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.GetRowF( -1 ) );
            Assert.Throws<ArgumentException>( () => _table.GetRowF( _table.RowCount ) );
        }

        [Test]
        public void GetRowF_ValidIndexAndNotAllColumnHaveValues_GetDoubleArrayWithElementsCountEqualToColumnsCount()
        {
            // Arrange
            var insertArray = new float[] { 1.2f };
            _table.AddRow( insertArray );
            var expectedArray = new float[] { 1.2f, 0f };

            // Act
            var currentArray = _table.GetRowF( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void GetRowF_AddValueForOneColumn_GetFloatArrayWithValuesForAllColumns()
        {
            // Arrange
            _table.AddValue( "Test1", 123f );
            var expectedArray = new float[] { 123f, 0f };

            // Act
            var currentArray = _table.GetRowF( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void GetRowF_InsertRowWithDoubleValues_GetFloatArray()
        {
            // Arrange
            var insertValues = new double[] { 123, 0 };
            _table.AddRow( insertValues );
            var expectedArray = new float[] { 123f, 0f };

            // Act
            var currentArray = _table.GetRowF( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void SetRow_InvalidIndex_ArgumentException()
        {
            var values = new double[] { 1.2, 23 };
            Assert.Throws<ArgumentException>( () => _table.SetRow( -1, values ) );
            Assert.Throws<ArgumentException>( () => _table.SetRow( _table.RowCount, values ) );
        }

        [Test]
        public void SetRow_NullRow_ArgumentException()
        {
            _table.AddRow( new double[] { 1.2 } );
            Assert.Throws<ArgumentException>( () => _table.SetRow( 0, ( double[] )null ) );
        }

        [Test]
        public void SetRow_SetValuesNotForAllColumns_GetDoubleArrayWithValuesForAllColumns()
        {
            // Arrange 
            _table.AddRow( new double[0] );
            var expectedArray = new double[] { 1.2, 30 };

            // Act
            _table.SetRow( _table.RowCount - 1, expectedArray );
            var currentArray = _table.GetRow( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void SetRow_SetValuesNotForAllColumns_GetFloatArrayWithValuesForAllColumns()
        {
            // Arrange 
            _table.AddRow( new float[0] );
            var expectedArray = new float[] { 1.2f, 30f };

            // Act
            _table.SetRow( _table.RowCount - 1, expectedArray );
            var currentArray = _table.GetRowF( _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedArray, currentArray );
        }

        [Test]
        public void SetRound_UnexistingColumn_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.SetRound( "NoColumn", 0 ) );
        }

        [Test]
        public void SetRound_NegativeRound_NoExceptions()
        {
            Assert.DoesNotThrow( () => _table.SetRound( "Test1", -10 ) );
        }

        [Test]
        public void SetRound_PositiveRound_NoExceptions()
        {
            Assert.DoesNotThrow( () => _table.SetRound( "Test1", 4 ) );
        }

        [Test]
        public void SetRound_SetRoundAtNotNegativeValueAndAddValue_GetRoundedValue()
        {
            // Arrange
            var value = 12.2222522225;
            var expectedValue = _table.Prefix + 12.22225.ToString() + _table.Postfix;

            // Act
            _table.SetRound( "Test1", 5 );
            _table.AddValue( "Test1", value );
            var currentValue = _table.PrintValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void SetRound_AddValueAndSetRoundAtNotNegativeValue_GetRoundedValue()
        {
            // Arrange
            var value = 12.2222522225;
            var expectedValue = _table.Prefix + 12.22225.ToString() + _table.Postfix;

            // Act
            _table.AddValue( "Test1", value );
            _table.SetRound( "Test1", 5 );
            var currentValue = _table.PrintValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void SetRound_SetRoundAtNegativeValueAndAddValue_GetNotRoundedValue()
        {
            // Arrange
            var value = 12.2222522225;
            var expectedValue = _table.Prefix + 12.2222522225.ToString() + _table.Postfix;

            // Act
            _table.SetRound( "Test1", -1 );
            _table.AddValue( "Test1", value );
            var currentValue = _table.PrintValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void SetRound_AddValueAndSetRoundAtNegativeValue_GetNotRoundedValue()
        {
            // Arrange
            var value = 12.2222522225;
            var expectedValue = _table.Prefix + 12.2222522225.ToString() + _table.Postfix;

            // Act
            _table.AddValue( "Test1", value );
            _table.SetRound( "Test1", -1 );
            var currentValue = _table.PrintValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void Calculate_NullFunction_ArgumentException()
        {
            Assert.Throws<ArgumentException>( () => _table.Calculate( "Test1", null ) );
        }

        [Test]
        public void Calculate_UnexistingColumn_ArgumentException()
        {
            double func( Dictionary<string, double> p ) => p[ "Test1" ] * 10;
            Assert.Throws<ArgumentException>( () => _table.Calculate( "NoColumn", func ) );
        }

        [Test]
        public void Calculate_UnexistingColumnInUsedFunction_ArgumentException()
        {
            _table.AddValue( "Test1", 1 );
            double func( Dictionary<string, double> p ) => p[ "NoColumn" ] * 10;
            Assert.Throws<ArgumentException>( () => _table.Calculate( "Test1", func ) );
        }

        [Test]
        public void Calculate_FunctionalTestUsingColumnsWithValue_OtherColumnsWasntChanged()
        {
            // Arrange
            var expectedValue = 1;
            _table.AddValue( "Test1", expectedValue );
            double func( Dictionary<string, double> p ) => p[ "Test1" ] * 10;

            // Act
            _table.Calculate( "Test2", func );
            var currentValue = _table.GetValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void Calculate_FunctionalTestUsingColumnsWithValue_RightValueAtCalculatedColumn()
        {
            // Arrange
            var baseValue = 1;
            _table.AddValue( "Test1", baseValue );
            var expectedValue = baseValue * 10;
            double func(Dictionary<string, double> p) => p[ "Test1" ] * 10;

            // Act
            _table.Calculate( "Test2", func );
            var currentValue = _table.GetValue( "Test2", _table.RowCount - 1);

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void Calculate_FunctionalTestUsingColumnsWithoutValues_UsedDefaultValues()
        {
            // Arrange
            _table.AddValue( "Test1", 1 );
            var expectedValue = 5;
            double func( Dictionary<string, double> p ) => p[ "Test2" ] + 5;

            // Act
            _table.Calculate( "Test1", func );
            var currentValue = _table.GetValue( "Test1", _table.RowCount - 1 );

            // Assert
            Assert.AreEqual( expectedValue, currentValue );
        }

        [Test]
        public void Calculate_NoRows_NoExceptions()
        {
            double func( Dictionary<string, double> p ) => p[ "Test1" ] * 10;
            Assert.DoesNotThrow( () => _table.Calculate( "Test2", func ) );
        }
    }
}
