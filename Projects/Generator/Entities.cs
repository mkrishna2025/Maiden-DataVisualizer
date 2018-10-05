using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Generator
{
	public class MaidenClassAttribute : Attribute
	{
		public string Table { get; set; }
		public bool EnableSearch { get; set; }

		public bool AllowImageSelection { get; set; }
	}

	public class MaidenFieldAttribute : Attribute
	{
		public bool IsNotPersist { get; set; }
		public bool EnableSearch { get; set; }
		public bool EnableWhere { get; set; }
		public bool PrimaryKey { get; set; }
		public string DBField { get; set; }
	}

	[MaidenClassAttribute(Table = "Xwall")]
	public class Activities
	{
		public string ActivityDescription;
		public string ActivityName;
		public int AdditiontalQuestionsCount;
		public int Data1IsNumeric;
		public string ETA;
		public bool IsBusinessActivity;
		public bool IsRepeatActivity;
		public bool IsSingleChoice;
		public bool IsSingleInstance;
		public string LastMessage;
		public string LastMessageBy;
		public string LastMessageTime;
		public string Location;
		public string Option1ColorCode;
		public string Option1Name;
		public int Option1Value;
		public bool Option1ValueChanged;
		public string Option2ColorCode;
		public string Option2Name;
		public int Option2Value;
		public bool Option2ValueChanged;
		public string Option3ColorCode;
		public string Option3Name;
		public int Option3Value;
		public bool Option3ValueChanged;
		public string Option4ColorCode;
		public string Option4Name;
		public int Option4Value;
		public bool Option4ValueChanged;
		public string OptionHeaderName;
		public string OwnerGuid;
		public string OwnerName;
		public int TotalInvitations;
		public string ActivityIcon;
		public int ActivityRepeatCount;
		public string ActivityThemeCode;
		public string BlobRef;
		public int ChoiceQuestionTotalResponses;
		public string CreatorGuid;
		public string CreatorName;
		public int Data1MaxValue;
		public int Data1MinValue;
		public string Data1Name;
		public int Data1TotalResponses;
		public int Data1TotalValue;
		public int Data1Value;
		public string Data2ColorCode;
		public bool Data2IsNumeric;
		public int Data2MaxValue;
		public int Data2MinValue;
		public string Data2Name;
		public int Data2TotalResponses;
		public int Data2TotalValue;
		public int Data2Value;
		public string Data3ColorCode;
		public bool Data3IsNumeric;
		public int Data3MaxValue;
		public int Data3MinValue;
		public string Data3Name;
		public int Data3TotalResponses;
		public int Data3TotalValue;
		public int Data3Value;
		public string Data4ColorCode;
		public bool Data4IsNumeric;
		public int Data4MaxValue;
		public int Data4MinValue;
		public string Data4Name;
		public int Data4TotalResponses;
		public int Data4TotalValue;
		public int Data4Value;
		public string Data5ColorCode;
		public bool Data5IsNumeric;
		public int Data5MaxValue;
		public int Data5MinValue;
		public string Data5Name;
		public int Data5TotalResponses;
		public int Data5TotalValue;
		public int Data5Value;
		public string ETag;
		public string EndDate;
		public string ExecutorGuid;
		public string ExecutorName;
		public string Option5ColorCode;
		public string Option5Name;
		public int Option5Value;
		public bool Option5ValueChanged;
		public string PartitionKey;
		public string RecurringFrequency;
		public string RepeatingDays;
		public string RepeatingHours;
		public string RowKey;
		public bool ShowSummary;
		public string StartDate;
		public string SubTasks;
		public bool SummaryBefore;
		public string TaskColorCode;
		public string Timestamp;
		public string ActivityGuid;
		public string ActivityType;
		public bool AddOnlyParentUsers;
		public string NickName;
		public string ParentActivityGuid;
		public string UserGuid;
		public bool IsAdmin;
		public bool IsParticipant;
		public string UserGuidTrimmed;
		public bool IsInvited;
		public int ParticipantCount;
		public int ResponseCount;
		public bool IsRecurringActivity;
		public int RecordCount;
		public string UnReadMessages;
		public bool IsActivityModified;
		public bool IsActivityDetailUpdated;
		public bool MessageCriticality;
		public int SubActivitiesCount;
		public string LastWriteTimeTicks;
		public string ActivityCurrentStatus;
		public string UserType;
		public string OwnerPhoneNumber;
		public string SubActivityStatusDetails;
		public string TimeStampLong;
		public bool Sinked_To_Server;
		public bool IsDeleted;
		public bool IsPinned;
		public string CreatedTime;
		public string CompletedBy;
		public string CompletedTime;
		public int IndexPosition;
		public string ProductId;
		public string ProductName;
	}

	[MaidenClassAttribute(EnableSearch = true)]
	public class Menu
	{
		[MaidenFieldAttribute(DBField = "SortOrder")]
		public int Order;

		[MaidenFieldAttribute(EnableSearch = true)]
		public string Label;

		public bool IsContainer;
		public bool AllowBreadCrum;
		public bool AllowAggregates;
		public bool AllowChildLevelNotifications;

		[MaidenFieldAttribute(EnableSearch = true, EnableWhere = true)]
		public string Name;
		public string Icon;
		public string Color;
		public string Childs;
		public string PartitionKey;
		public string RowKey;


		public string Timestamp;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		//public string ETag;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		public string AccessType;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		public bool AddChildLevelParticipantToParent;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		public bool IsAddedToUser;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		public bool IsParticipantCanAddActivity;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		public string ParentTemplate;

		//[MaidenFieldAttribute(IsNotPersist = true)]
		public string TemplateType;
	}

	[MaidenClassAttribute(Table = "Participants")]
	public class Participant
	{
		[MaidenFieldAttribute(PrimaryKey = true)]
		public int ID;
		[MaidenFieldAttribute(EnableWhere = true)]
		public string ActivityGuid;
		public string CommunityGuid;
		public bool IsAdmin;
		public bool IsCommunity;
		public bool IsInvitation;
		public bool IsParticipant;
		public string OwnerGuid;
		public string OwnerName;
		public string PartitionKey;
		public string RowKey;
		[MaidenFieldAttribute(EnableWhere = true)]
		public string UserGuid;
		public string UserGuidTrimmed;
		public string PhoneNumber;
		public string NickName;
		public string ImageObj;
		public bool IsZibme_User;
		public bool IsZibme_Community;
		public bool Sinked_To_Server;
		public bool IsDeleted;
		public bool Selected;
		public bool IsFromContacts;
		public string RegistrationType;

		//public bool IsInherited;
		//public bool IsSpecialInvitation;
		//public int Option1Value;
		//public int Option2Value;
		//public int Option3Value;
		//public int Option4Value;
		//public int Option5Value;
		//public bool ChoiceQuestionAnswered;
		//public string RegistrationType;
		//public string Timestamp;
		//public string Data1Value;
		//public string Data2Value;
		//public string Data3Value;
		//public string Data4Value;
		//public string Data5Value;
		//public string ETag;
	}

	public class ChatMessage
	{
		public bool Selected;
		public bool IsRead;
		public bool IsSinked_To_Server;

		public string Identifier;
		public string Chat_id;
		public string Text;
		public DateTime Date;
		// Recieving
		[MaidenFieldAttribute(EnableWhere = true)]
		public string ActivityGuid;

		public string ETag;
		public string MessageBody;
		public string MessageFrom;
		public string MessageStatus;

		[MaidenFieldAttribute(EnableWhere = true)]
		public string MessageTime;

		public string MessageType;

		public string Option1Name;
		public string Option1Value;

		[MaidenFieldAttribute(EnableWhere = true)]
		public string SenderId;
		public string SystemGenerated;
		public string Timestamp;
		public string ParentActivity;
		public string Last_message;
	}

	public class ActivityNotification
	{
		[MaidenFieldAttribute(PrimaryKey = true)]
		public int ID;

		[MaidenField(EnableWhere = true)]
		public string ActivityGuid;

		public string ParentActivityGuid;
		public string MessageID;
		public string Message;
		public string MessageTime;
		public string SystemGenerated;
		public string MessageSenderId;
		public string MessageType;
		public string MessageSenderName;
		public bool IsRead;

	}
}
