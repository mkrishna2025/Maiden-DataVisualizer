//
//  MenuModal.h
//  Maiden
//
//  Created by Kamal on 7/31/17.
//  Copyright © 2017 BookMEDS. All rights reserved.
//

#import <JSONModel/JSONModel.h>

@interface MenuModal : JSONModel

@property (nonatomic, copy) NSString *Order;
@property (nonatomic, copy) NSString *Label;
@property (nonatomic,assign) BOOL IsContainer;
@property (nonatomic, assign) BOOL AllowBreadCrum;
@property (nonatomic, assign) BOOL AllowAggregates;
@property (nonatomic, assign) BOOL AllowChildLevelNotifications;
@property (nonatomic, copy) NSString *Name;
@property (nonatomic, copy) NSString *Icon;
@property (nonatomic, copy) NSString *Color;
@property (nonatomic, copy) NSString *Childs;
@property (nonatomic, copy) NSString *PartitionKey;
@property (nonatomic, copy) NSString *RowKey;
@property (nonatomic, copy) NSString *Timestamp;
@property (nonatomic, copy) NSString *ETag;
@property (nonatomic, copy) NSString *AccessType;
@property (nonatomic, assign) BOOL AddChildLevelParticipantToParent;
@property (nonatomic, assign) BOOL IsAddedToUser;
@property (nonatomic, assign) BOOL IsParticipantCanAddActivity;
@property (nonatomic, copy) NSString *ParentTemplate;
@property (nonatomic, copy) NSString *TemplateType;

-(NSMutableArray *)convertDictionaryToObjectArray:(NSDictionary *)responseDictionary;

@end
