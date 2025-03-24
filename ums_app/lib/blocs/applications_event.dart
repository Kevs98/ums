part of 'applications_bloc.dart';

abstract class ApplicationsEvent extends Equatable {
  const ApplicationsEvent();

  @override
  List<Object> get props => [];
}

class GetApplicationsEvent extends ApplicationsEvent {
  const GetApplicationsEvent();

  @override
  List<Object> get props => [];
}
