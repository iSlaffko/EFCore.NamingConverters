using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using EFCore.NamingConverters.Tests.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EFCore.NamingConverters.Tests
{
    public class ConvertNamingShould
    {
        private Fixture _fixture;
        private ModelBuilder _modelBuilder;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _modelBuilder = _fixture.Create<ModelBuilder>();
            _modelBuilder.ApplyConfiguration(new ParentConfiguration());
            _modelBuilder.ApplyConfiguration(new ChildConfiguration());
        }

        [TestCase(NamingConverterFlags.None)]
        [TestCase(NamingConverterFlags.Schemas)]
        [TestCase(NamingConverterFlags.Tables)]
        [TestCase(NamingConverterFlags.Keys)]
        [TestCase(NamingConverterFlags.ForeignKeys)]
        [TestCase(NamingConverterFlags.Indexes)]
        [TestCase(NamingConverterFlags.Columns)]
        [TestCase(NamingConverterFlags.All)]
        [TestCase(NamingConverterFlags.Schemas | NamingConverterFlags.Tables)]
        public void ShouldOnlyConvertWhatItWasAsked(NamingConverterFlags flags)
        {
            _modelBuilder.ConvertNaming(x => x.ToLower(), flags);
            var dict = new Dictionary<NamingConverterFlags, List<string>>
            {
                { NamingConverterFlags.Schemas, this.GetSchemas() },
                { NamingConverterFlags.Tables, this.GetTables() },
                { NamingConverterFlags.Keys, this.GetKeys() },
                { NamingConverterFlags.ForeignKeys, this.GetForeignKeys() },
                { NamingConverterFlags.Indexes, this.GetIndexes() },
                { NamingConverterFlags.Columns, this.GetColumns() }
            };

            foreach (var item in dict)
            {
                item.Value.Should().NotBeEmpty();

                if (flags.HasFlag(item.Key))
                {
                    item.Value.Should().OnlyContain(x => !x.Any(char.IsUpper));
                }
                else
                {
                    item.Value.Should().OnlyContain(x => x.Any(char.IsUpper));
                }
            }
        }

        private List<string> GetTables() =>
            _modelBuilder.Model.GetEntityTypes()
                .Select(x => x.GetTableName())
                .ToList();

        private List<string> GetColumns() =>
            _modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetProperties())
                .Select(x => x.GetColumnName())
                .ToList();

        private List<string> GetKeys() =>
            _modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetKeys())
                .Select(x => x.GetName())
                .ToList();

        private List<string> GetForeignKeys() =>
            _modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetForeignKeys())
                .Select(x => x.GetConstraintName())
                .ToList();

        private List<string> GetIndexes() =>
            _modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetIndexes())
                .Select(x => x.GetName())
                .ToList();

        private List<string> GetSchemas() =>
            _modelBuilder.Model.GetEntityTypes()
                .Select(x => x.GetSchema())
                .ToList();
    }
}
