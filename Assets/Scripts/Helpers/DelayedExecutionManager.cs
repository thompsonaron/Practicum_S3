using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DelayedExecutionManager
{
    public static int QuickSortMargin = 10;
    //Singleton
    private static DelayedExecutionManager SharedInstance
    {
        get
        {
            if (_sharedInstance == null)
            {
                _sharedInstance = new DelayedExecutionManager();
                GameTicker.SharedInstance.Update += UpdateExecutionList;
            }

            return _sharedInstance;
        }
        set
        {
            _sharedInstance = value;
        }
    }
    private static DelayedExecutionManager _sharedInstance;

    //Constructor
    public DelayedExecutionManager()
    {
        actionList = new List<DelayedExecutionTicket>();
        executedTickets = new List<DelayedExecutionTicket>();
    }

    //Private properties
    private List<DelayedExecutionTicket> actionList;

    private List<DelayedExecutionTicket> executedTickets;

    //Needs to be called on Update to be able to execute stored actions timely
    public static void UpdateExecutionList()
    {
        if (SharedInstance.actionList.Count == 0)
        {
            return;
        }

        SharedInstance.executeTicketOnIndexIfNeeded(0);

        if (SharedInstance.executedTickets.Count > 0)
        {
            for (int i = 0; i < SharedInstance.executedTickets.Count; i++)
            {
                SharedInstance.actionList.Remove(SharedInstance.executedTickets[i]);
                //Debug.Log("Removing: " + ticket.name);
            }

            SharedInstance.executedTickets.Clear();
        }
    }

    //Sets action for delayed execution, 3 overloads
    public static DelayedExecutionTicket ExecuteActionAfterDelay(int delay, Action block)
    {
        return ExecuteActionAfterDelay(delay, block, 0);
    }

    public static DelayedExecutionTicket ExecuteActionAfterDelay(int delay, Action block, int referenceID)
    {
        return ExecuteActionAfterDelay(delay, block, referenceID.ToString(), referenceID);
    }

    public static DelayedExecutionTicket ExecuteActionAfterDelay(int delay, Action block, string name, int referenceID = 0)
    {
        return SharedInstance.StoreAndExecuteActionAfterDelay(delay, block, name, referenceID);
    }


    //Cancels the ticket, hence the name :) ticket can be null
    public static void CancelTicket(DelayedExecutionTicket ticket)
    {
        SharedInstance.actionList.Remove(ticket);
    }

    private void executeTicketOnIndexIfNeeded(int index)
    {
        if (SharedInstance.actionList[index].executionTime <= Time.time)
        {
            var ticket = actionList[index];
            executeTicket(ticket);

            if (actionList.Count > index + 1)
            {
                //check next element 
                executeTicketOnIndexIfNeeded(index + 1);
            }
            //add ticket to disposal list
            executedTickets.Add(ticket);
        }
    }

    //
    //Private methods
    //
    private void executeTicket(DelayedExecutionTicket ticket)
    {
        ticket.block();
    }

    private void removeTicket(DelayedExecutionTicket ticket)
    {
        actionList.Remove(ticket);
    }

    private void addTicket(DelayedExecutionTicket ticket)
    {
        //Debug.Log("Standard");
        //if list is empty
        if (actionList.Count == 0)
        {
            actionList.Add(ticket);
            return;
        }

        //if it should be first
        if (actionList[0].executionTime > ticket.executionTime)
        {
            actionList.Insert(0, ticket);
            return;
        }
        //if last
        if (actionList[actionList.Count - 1].executionTime <= ticket.executionTime)
        {
            actionList.Insert(actionList.Count, ticket);
            return;
        }
        //anywhere in the middle
        for (int i = actionList.Count - 1; i >= 0; i--)
        {
            if (ticket.executionTime >= actionList[i].executionTime)
            {
                actionList.Insert(i + 1, ticket);
                return;
            }
        }
    }

    private void addTicket(DelayedExecutionTicket ticket, int fromIndex, int toIndex)
    {
        //Debug.Log("Quick");
        //if list is empty
        if (actionList.Count == 0)
        {
            actionList.Add(ticket);
            return;
        }
        //if it should be first
        if (actionList[fromIndex].executionTime >= ticket.executionTime)
        {
            actionList.Insert(fromIndex, ticket);
            return;
        }
        //if last
        if (actionList[toIndex].executionTime <= ticket.executionTime)
        {
            actionList.Insert(toIndex + 1, ticket);
            return;
        }

        var anchor = (int)((float)(toIndex - fromIndex) / 2);

        if (ticket.executionTime > actionList[anchor + fromIndex].executionTime)
        {
            if (ticket.executionTime <= actionList[fromIndex + anchor + 1].executionTime)
            {
                //lucky us
                actionList.Insert(fromIndex + anchor + 1, ticket);
                return;
            }
            addTicket(ticket, fromIndex + anchor, toIndex);

        }
        else
        {
            addTicket(ticket, fromIndex, anchor);
        }
    }

    private DelayedExecutionTicket StoreAndExecuteActionAfterDelay(int delay, Action block, string name = "name", int referenceID = 0)
    {
        var ticket = new DelayedExecutionTicket();
        ticket.block = block;
        ticket.name = name;
        ticket.referenceID = referenceID;
        //convert miliseconds to seconds
        float delayInSeconds = (float)delay / 1000.0f;

        ticket.executionTime = Time.time + delayInSeconds;

        //switch to quicksort when needed
        if (actionList.Count < QuickSortMargin)
        {
            addTicket(ticket);
        }
        else
        {
            addTicket(ticket, 0, actionList.Count - 1);
        }

        return ticket;
    }

#if DEBUG
    //helper
    public static void PrintActionList()
    {
        for (int i = 0; i < SharedInstance.actionList.Count; i++)
        {
            Debug.Log("Item: " + i + " - " + SharedInstance.actionList[i].executionTime);
        }
    }
#endif
}


public class DelayedExecutionTicket
{
    public Action block;
    public string name;
    public int referenceID;
    public float executionTime;
}