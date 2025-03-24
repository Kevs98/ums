part of 'auth_bloc.dart';

abstract class AuthState extends Equatable {
  const AuthState();

  @override
  List<Object> get props => [];
}

class AuthInitial extends AuthState {}

class AuthLoading extends AuthState {}

class AuthSuccess extends AuthState {
  final User loginResponse;

  const AuthSuccess(this.loginResponse);

  @override
  List<Object> get props => [loginResponse];
}

class AuthFailure extends AuthState {
  final String error;

  const AuthFailure(this.error);

  @override
  List<Object> get props => [error];
}

class OtpValidated extends AuthState {
  final String token;

  const OtpValidated(this.token);

  @override
  List<Object> get props => [token];
}
