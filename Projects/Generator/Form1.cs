using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Generator
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		// https://docs.microsoft.com/en-us/azure/storage/storage-dotnet-how-to-use-tables
	
		public string GenerateCreateString(string entity, Type type)
		{
			string table = entity;

			var tblName = type.GetCustomAttribute<MaidenClassAttribute>();
			if (null != tblName)
			{
				if (!string.IsNullOrWhiteSpace(tblName.Table))
				{
					table = tblName.Table;
				}
			}
			StringBuilder str = new StringBuilder("create table if not exists " + table + "( ");

			var fields = type.GetFields().ToList();

			List<string> items = new List<string>();

			fields.ForEach(field =>
			{
				var dbColumn = (field.Name);

				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();

				if (null != attribute && !string.IsNullOrWhiteSpace(attribute.DBField))
				{
					dbColumn = (attribute.DBField);
				}

				if (attribute != null && attribute.PrimaryKey)
				{
					items.Add(dbColumn + " INTEGER PRIMARY KEY AUTOINCREMENT");
				}
				else
				{
					if (field.FieldType.Equals(typeof(int)))
					{
						items.Add(dbColumn + " INTEGER");
					}
					else if (field.FieldType.Equals(typeof(bool)))
					{
						items.Add(dbColumn + " BOOL");
					}
					else
					{
						items.Add(dbColumn + " VARCHAR(255)");
					}
				}

			});

			str.Append(string.Join(", ", items.ToArray()));
			str.Append(" )");

			return str.ToString();
		}

		public string GenerateAlterString(string entity, Type type)
		{
			string table = entity;

			var tblName = type.GetCustomAttribute<MaidenClassAttribute>();
			if (null != tblName)
			{
				if (!string.IsNullOrWhiteSpace(tblName.Table))
				{
					table = tblName.Table;
				}
			}
			StringBuilder str = new StringBuilder("[NSString stringWithFormat: @\"insert into  " + table + "(");

			var fields = type.GetFields().ToList();

			List<string> items = new List<string>();

			return str.ToString();
		}

		public string GenerateInsertString(string entity, Type type)
		{
			string table = entity;

			var tblName = type.GetCustomAttribute<MaidenClassAttribute>();
			if (null != tblName)
			{
				if (!string.IsNullOrWhiteSpace(tblName.Table))
				{
					table = tblName.Table;
				}
			}
			StringBuilder str = new StringBuilder("[NSString stringWithFormat: @\"insert into  " + table + "(");

			var fields = type.GetFields().ToList();

			List<string> items = new List<string>();

			fields.ForEach(field =>
			{
				string column = ToCamelCase(field.Name);
				bool isProcess = true;
				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();
				if (null != attribute)
				{
					if (attribute.IsNotPersist)
					{
						isProcess = false;
					}
					if (!string.IsNullOrWhiteSpace(attribute.DBField))
					{
						column = ToCamelCase(attribute.DBField);
					}
				}
				if (isProcess)
				{
					items.Add(column);
				}
			});

			str.Append(string.Join(", ", items.ToArray()));

			items.Clear();

			str.Append(")values(");

			fields.ForEach(field =>
			{
				bool isProcess = true;
				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();
				if (null != attribute)
				{
					if (attribute.IsNotPersist)
					{
						isProcess = false;
					}
				}
				if (isProcess)
				{
					if (field.FieldType.Equals(typeof(int)) || field.FieldType.Equals(typeof(bool)))
					{
						items.Add("%d");
					}
					else
					{
						items.Add("\\\"%@\\\"");
					}
				}
			});

			str.Append(string.Join(", ", items.ToArray()));

			str.Append(")\",");

			items.Clear();

			fields.ForEach(field =>
			{
				bool isProcess = true;
				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();
				if (null != attribute)
				{
					if (attribute.IsNotPersist)
					{
						isProcess = false;
					}
				}
				if (isProcess)
				{
					items.Add(ToCamelCase(entity) + "Item." + field.Name);
				}
			});

			str.Append(string.Join(", ", items.ToArray()));

			str.Append("];");

			// NSString* query = [NSString stringWithFormat: @"insert into  Activities(activityDescription,activityName)values(\" %@\",\"%@\")", taskActivityModel.activityDescription, taskActivityModel.activityName];
			return str.ToString();
		}

		public string GenerateUpdateString(string entity, Type type)
		{
			string table = entity;

			var tblName = type.GetCustomAttribute<MaidenClassAttribute>();
			if (null != tblName)
			{
				if (!string.IsNullOrWhiteSpace(tblName.Table))
				{
					table = tblName.Table;
				}
			}
			StringBuilder str = new StringBuilder("[NSString stringWithFormat: @\"update " + table + " set ");

			var fields = type.GetFields().ToList();

			List<string> items = new List<string>();
			List<string> whereItems = new List<string>();
			List<string> nameItems = new List<string>();

			fields.ForEach(field =>
			{
				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();
				if (attribute != null)
				{
					var dbColumn = ToCamelCase(field.Name);

					if (!string.IsNullOrWhiteSpace(attribute.DBField))
					{
						dbColumn = ToCamelCase(attribute.DBField);
					}


					var string1 = dbColumn + " = ";
					if (attribute.EnableWhere)
					{
						if (whereItems.Count == 0)
						{
							string1 = " where " + string1;
						}
						if (field.FieldType.Equals(typeof(int)) || field.FieldType.Equals(typeof(bool)))
						{
							string1 += "\'%d\'";
						}
						else
						{
							string1 += "\'%@\'";
						}
						whereItems.Add(string1);
					}
					else
					{
						var string2 = dbColumn + " = ";
						if (field.FieldType.Equals(typeof(int)) || field.FieldType.Equals(typeof(bool)))
						{
							string2 += "\'%d\'";
						}
						else
						{
							string2 += "\'%@\'";
						}
						items.Add(string2);
					}
				}
				else
				{
					var string2 = ToCamelCase(field.Name) + " = ";
					if (field.FieldType.Equals(typeof(int)) || field.FieldType.Equals(typeof(bool)))
					{
						string2 += "\'%d\'";
					}
					else
					{
						string2 += "\'%@\'";
					}
					items.Add(string2);
				}
			});



			str.Append(string.Join(",", items.ToArray()));

			str.Append(string.Join(" and ", whereItems.ToArray()));

			items.Clear();

			str.Append("\"");

			str.Append(",");

			fields.ForEach(field =>
			{
				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();
				if (attribute != null)
				{
					if (attribute.EnableWhere)
					{
						nameItems.Add(ToCamelCase(entity) + "Item." + field.Name);
					}
					else
					{
						items.Add(ToCamelCase(entity) + "Item." + field.Name);
					}
				}
				else
				{
					items.Add(ToCamelCase(entity) + "Item." + field.Name);
				}

			});

			nameItems.ForEach(field =>
			{
				items.Add(field);
			});

			str.Append(string.Join(", ", items.ToArray()));

			str.Append("];");

			return str.ToString();
		}

		public string GenerateProperties(string entity, Type type)
		{
			StringBuilder str = new StringBuilder();
			var fields = type.GetFields().ToList();
			List<string> items = new List<string>();
			items.Add("-(" + entity + "Modal *) convertDBRowToToolBar:(FMResultSet *)rs {");
			items.Add(entity + "Modal * " + ToCamelCase(entity) + "Obj = [[" + entity + "Modal alloc] init];");
			fields.ForEach(field =>
			{
				string dbColumn = ToCamelCase(field.Name);

				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();
				if (attribute != null)
				{
					if (!string.IsNullOrWhiteSpace(attribute.DBField))
					{
						dbColumn = ToCamelCase(attribute.DBField);
					}
				}

				if (field.FieldType.Equals(typeof(bool)))
				{
					var stringValue = ToCamelCase(entity) + "Obj." + field.Name + " = " + "[[rs stringForColumn:@\"" + dbColumn + "\"]boolValue];";
					items.Add(stringValue);
				}
				else if (field.FieldType.Equals(typeof(int)))
				{
					var stringValue = ToCamelCase(entity) + "Obj." + field.Name + " = " + "[[rs stringForColumn:@\"" + dbColumn + "\"]intValue];";
					items.Add(stringValue);
				}
				else
				{
					var stringValue = ToCamelCase(entity) + "Obj." + field.Name + " = " + "[rs stringForColumn:@\"" + dbColumn + "\"];";
					items.Add(stringValue);
				}
			});
			items.Add("return " + ToCamelCase(entity) + "Obj;");
			str.Append(string.Join("\n", items.ToArray()));

			str.Append("\n}");
			return str.ToString();
		}

		public string GenerateConversationString(string entity, Type type)
		{
			StringBuilder str = new StringBuilder();
			var fields = type.GetFields().ToList();
			List<string> items = new List<string>();
			fields.ForEach(field =>
			{
				if (field.FieldType.Equals(typeof(bool)))
				{
					var stringValue = ToCamelCase(entity) + "Obj." + field.Name + " = " + "[[dict valueForKey:@\"" + field.Name + "\"] boolValue];";
					items.Add(stringValue);
				}
				else if (field.FieldType.Equals(typeof(int)))
				{
					var stringValue = ToCamelCase(entity) + "Obj." + field.Name + " = " + "[[dict valueForKey:@\"" + field.Name + "\"] intValue];";
					items.Add(stringValue);
				}
				else
				{
					var stringValue = ToCamelCase(entity) + "Obj." + field.Name + " = " + "[self getObjectFromDictionary:dict withKey:@\"" + field.Name + "\"];";
					items.Add(stringValue);
				}
			});

			str.AppendLine(string.Join("\n", items.ToArray()));
			return str.ToString();

		}
		private string ToCamelCase(string str)
		{
			return str[0].ToString().ToLower() + str.Substring(1);
		}

		private string ToVariableCase(string str)
		{
			return str[0].ToString().ToUpper() + str.Substring(1);
		}

		public string GenerateJsonRecord(string entity, Type type)
		{
			string table = entity;

			var tblName = type.GetCustomAttribute<MaidenClassAttribute>();
			if (null != tblName)
			{
				if (!string.IsNullOrWhiteSpace(tblName.Table))
				{
					table = tblName.Table;
				}
			}
			StringBuilder str = new StringBuilder("NSMutableDictionary * jsonRecord = [[NSMutableDictionary alloc] init];");
			var fields = type.GetFields().ToList();
			List<string> items = new List<string>();
			fields.ForEach(field =>
			{
				var attribute = field.GetCustomAttribute<MaidenFieldAttribute>();

				if ((null != attribute) && attribute.EnableWhere)
				{
					items.Add("\n[jsonRecord setObject:" + ToCamelCase(entity) + "Item." + field.Name + " forKey:@\"" + field.Name + "\"];");
				}
			});

			items.Add("\n BOOL exists = [self isExists:jsonRecord inTable:@\"" + table + "\"];");
			str.AppendLine(string.Join("", items.ToArray()));

			return str.ToString();

		}
		private Dictionary<String, String> ConstructParams(string entity, Type type)
		{
			string entityLower = ToCamelCase(entity);

			var replaceParams = new Dictionary<String, String>();
			replaceParams.Add("@string.entitiy@", entity);
			replaceParams.Add("@string.entitiy.lower@", entityLower);

			StringBuilder propertiesString = new StringBuilder();

			StringBuilder searchHString = new StringBuilder();
			StringBuilder searchMString = new StringBuilder();

			var fields = type.GetFields().ToList();

			fields.ForEach(field =>
			{
				if (field.FieldType.Equals(typeof(bool)))
				{
					propertiesString.AppendLine("@property (nonatomic, assign) BOOL " + field.Name + ";");
				}
				else if (field.FieldType.Equals(typeof(int)))
				{
					propertiesString.AppendLine("@property (nonatomic) int " + field.Name + ";");
				}
				else if (field.FieldType.Equals(typeof(DateTime)))
				{
					propertiesString.AppendLine("@property (nonatomic) NSDate " + field.Name + ";");
				}
				else
				{
					propertiesString.AppendLine("@property (nonatomic, strong) NSString *" + field.Name + ";");
				}

				var fieldAttr = field.GetCustomAttribute<MaidenFieldAttribute>();

				if (null != fieldAttr)
				{
					if (fieldAttr.EnableSearch)
					{
						searchHString.AppendLine("-(" + entity + "Modal *)get" + entity + "For" + field.Name + ":(NSString *)value;");
						searchMString.AppendLine("-(" + entity + "Modal *) get" + entity + "For" + field.Name + ":(NSString *)value{");
						searchMString.AppendLine("return [self get" + entity + "ForColumn:@\"" + field.Name + "\" WithValue:value];}");
					}
				}
			});

			string table = entity;

			var attribute = type.GetCustomAttribute<MaidenClassAttribute>();
			StringBuilder imageConversion = new StringBuilder();

			if (null != attribute)
			{
				if (!string.IsNullOrWhiteSpace(attribute.Table))
				{
					table = attribute.Table;
				}

				if (attribute.AllowImageSelection)
				{
					imageConversion.AppendLine(entityLower + "Obj.selected = NO;");
					imageConversion.AppendLine(entityLower + "Obj.isFromContacts = NO;");
					imageConversion.AppendLine(entityLower + "Obj.imageObj = [UIImage imageNamed:@\"\"];");
				}

			}

			replaceParams.Add("@string.table@", table);

			replaceParams.Add("@string.properties@", propertiesString.ToString());

			replaceParams.Add("@string.insert.string@", GenerateInsertString(entity, type));

			replaceParams.Add("@string.Update.string@", GenerateUpdateString(entity, type));

			replaceParams.Add("@string.generateProperties.string@", GenerateProperties(entity, type));

			replaceParams.Add("@string.search.h@", searchHString.ToString());

			replaceParams.Add("@string.search.m@", searchMString.ToString());

			replaceParams.Add("@string.generateConversationString.string@", GenerateConversationString(entity, type));

			replaceParams.Add("@string.generateImageConversationString.string@", imageConversion.ToString());

			replaceParams.Add("@string.generateJsonRecord.string@", GenerateJsonRecord(entity, type));

			replaceParams.Add("@string.create.string@", GenerateCreateString(entity, type));
			replaceParams.Add("@string.alter.string@", GenerateAlterString(entity, type));


			return replaceParams;
		}

		public void GenerateEntity(string entity, Type type)
		{
			// MenuDAO.h
			var template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Templates/TemplateHDAO.txt"));

			var replaceParams = ConstructParams(entity, type);

			string generatedString = template;

			foreach (var keyValuePair in replaceParams)
			{
				generatedString = generatedString.Replace(keyValuePair.Key, keyValuePair.Value);
			}

			File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Generated/" + entity + "DAO.h"), generatedString);

			//MenuModal.h
			template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Templates/TemplateHModal.txt"));

			generatedString = template;

			foreach (var keyValuePair in replaceParams)
			{
				generatedString = generatedString.Replace(keyValuePair.Key, keyValuePair.Value);
			}

			File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Generated/" + entity + "Modal.h"), generatedString);

			//MenuModal.m
			template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Templates/TemplateMModal.txt"));

			generatedString = template;

			foreach (var keyValuePair in replaceParams)
			{
				generatedString = generatedString.Replace(keyValuePair.Key, keyValuePair.Value);
			}

			File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Generated/" + entity + "Modal.m"), generatedString);

			//MenuDAO.m
			template = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Templates/TemplateMDAO.txt"));

			generatedString = template;

			foreach (var keyValuePair in replaceParams)
			{
				generatedString = generatedString.Replace(keyValuePair.Key, keyValuePair.Value);
			}

			File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "../../Generated/" + entity + "DAO.m"), generatedString);
		}

		public void GenerateFiles()
		{
			GenerateEntity("Menu", typeof(Menu));
			GenerateEntity("Activities", typeof(Activities));
			GenerateEntity("ChatMessage", typeof(ChatMessage));
			GenerateEntity("Participant", typeof(Participant));
			GenerateEntity("ActivityNotification", typeof(ActivityNotification));

		}

		private void Form1_Load(object sender, EventArgs e)
		{
			GenerateFiles();
		}
	}
}
