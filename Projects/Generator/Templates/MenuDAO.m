//
//  MenuDAO.m
//  Maiden
//
//  Created by Kamal on 7/31/17.
//  Copyright © 2017 BookMEDS. All rights reserved.
//

#import "MenuDAO.h"
#import "MenuModal.h"

@implementation MenuDAO

-(BOOL)save:(NSArray *)menuItems{
    
    FMDatabaseQueue *dbQueue_Menu = [self fmDataQueue];
    
    [dbQueue_Menu inDatabase:^(FMDatabase *database) {
        
        
        for (MenuModal *menuItem in menuItems)
        {
            BOOL exists = [self isExists:@"name" WithValue:menuItem.Name withTable:@"Menu"];
            
            NSString *query;
            
            if (exists == YES)
            {
                //  exists so Update
                NSLog(@"Menu items exists so Update");
                
                query = [NSString stringWithFormat:@"update Menu set partition_key = '%@',rowKey = '%@',timestampp = '%@',allowBreadCrum = '%d',allowAggregates = '%d',allowChildLevelNotifications = '%d',childs = '%@',color = '%@', icon = '%@', isContainer = '%d', label = '%@', name = '%@', orderNumber = '%@' where name = '%@'",menuItem.PartitionKey,menuItem.RowKey,menuItem.Timestamp,menuItem.AllowBreadCrum,menuItem.AllowAggregates,menuItem.AllowChildLevelNotifications,menuItem.Childs,menuItem.Color,menuItem.Icon,menuItem.IsContainer,menuItem.Label,menuItem.Name,menuItem.Order,menuItem.Name];
                
            }
            else
            {
                // not exists so insert
                NSLog(@"not exists so insert");
                
                query = [NSString stringWithFormat:@"insert into Menu (partitionKey,rowKey,timestamp,allowBreadCrum,allowAggregates,allowChildLevelNotifications,childs,color,icon,isContainer,label,name,orderId)values(\"%@\",\"%@\",\"%@\",%d,%d,%d,\"%@\",\"%@\",\"%@\",%d,\"%@\",\"%@\",%@)",menuItem.PartitionKey,menuItem.RowKey,menuItem.Timestamp,menuItem.AllowBreadCrum,menuItem.AllowAggregates,menuItem.AllowChildLevelNotifications,menuItem.Childs,menuItem.Color,menuItem.Icon,menuItem.IsContainer,menuItem.Label,menuItem.Name,menuItem.Order];
                
            }
            
            
            int result = [db executeUpdate:query];
            
            if (!result) {
                NSLog(@"Failed To Insert -444-\"%@\"",query);
            }else{
                NSLog(@"Menu InserttSUCC -- %d",result);
            }
        }
        
        
    }];
    
    return TRUE;
    
}

-(MenuModal *)convertDBRowToToolBar:(FMResultSet *)rs {
    MenuModal * menuObj = [[MenuModal alloc] init];
    
    menuObj.Order = [rs stringForColumn:@"orderId"];
    menuObj.Name = [rs stringForColumn:@"name"];
    menuObj.Label = [rs stringForColumn:@"label"];
    menuObj.IsContainer = [rs stringForColumn:@"isContainer"];
    menuObj.AllowAggregates = [rs stringForColumn:@"allowAggregates"];
    menuObj.AllowBreadCrum = [rs stringForColumn:@"allowBreadCrum"];
    menuObj.AllowChildLevelNotifications = [rs stringForColumn:@"allowChildLevelNotifications"];
    menuObj.Icon = [rs stringForColumn:@"icon"];
    menuObj.Color = [rs stringForColumn:@"color"];
    menuObj.Childs = [rs stringForColumn:@"childs"];
    menuObj.PartitionKey = [rs stringForColumn:@"partitionKey"];
    menuObj.RowKey = [rs stringForColumn:@"rowKey"];
    menuObj.Timestamp = [rs stringForColumn:@"timestamp"];
    menuObj.ETag = [rs stringForColumn:@"eTag"];

    return menuObj;
}


-(NSMutableArray *)getToolbarMenuItems{
    
    
    NSString * query = [NSString stringWithFormat:@"select * from Menu"];
    
    NSMutableArray *menuItems =[[NSMutableArray alloc] init];
    
    FMResultSet *rs = [db executeQuery:query];
    
    while (rs.next )
    {
        
        MenuModal * menuObj = [self convertDBRowToToolBar:rs];
        [menuItems addObject:menuObj];
        
    }
    return menuItems;
    
}

-(MenuModal *)getMenuForColumn:(NSString *)column WithValue:(NSString*)value{

    NSString * query = [NSString stringWithFormat:@"select * from Menu where %@ = '%@'",column, value];
    
    MenuModal * menuObj = [[MenuModal alloc] init];
    
    FMResultSet *rs = [db executeQuery:query];
    
    while (rs.next )
    {
        menuObj = [self convertDBRowToToolBar:rs];
    }
    
    return menuObj;
}

/*
-(MenuModal *)getMenuForName:(NSString *)name{
    
    return [self getMenuForColumn:@"name" WithValue:name];
}


-(MenuModal *)getMenuForLabel:(NSString *)name{
    
    return [self getMenuForColumn:@"label" WithValue:name];
}


- (BOOL)getCurrentToolBarName:(NSString *)name
{
    return [self isExists:@"name" WithValue: name];
}*/


@end
