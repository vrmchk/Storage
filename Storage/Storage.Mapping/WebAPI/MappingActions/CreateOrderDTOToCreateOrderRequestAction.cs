﻿using AutoMapper;
using Storage.BLL.Requests.Order;
using Storage.BLL.Requests.Order.Models;
using Storage.Common.Models.DTOs.Order;

namespace Storage.Mapping.WebAPI.MappingActions;

public class CreateOrderDTOToCreateOrderRequestAction : IMappingAction<CreateOrderDTO, CreateOrderRequest>
{
    public void Process(CreateOrderDTO source, CreateOrderRequest destination, ResolutionContext context)
    {
        destination.OrderSelections = context.Mapper.Map<List<CreateOrderSelectionModel>>(source.OrderSelections);
    }
}