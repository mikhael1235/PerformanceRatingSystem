﻿using MediatR;
using PerformanceRatingSystem.Application.Dtos;

namespace PerformanceRatingSystem.Application.Requests.Commands;

public record CreateActualPerformanceResultCommand(ActualPerformanceResultForCreationDto ActualPerformanceResult) : IRequest;
