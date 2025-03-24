part of 'auth_bloc.dart';

abstract class AuthEvent extends Equatable {
  const AuthEvent();

  @override
  List<Object> get props => [];
}

class LoginEvent extends AuthEvent {
  final String username;
  final String password;

  const LoginEvent({required this.username, required this.password});

  @override
  List<Object> get props => [username, password];
}

class CheckAuthStatusEvent extends AuthEvent {}

class ValidateOtpEvent extends AuthEvent {
  final String otp;
  final String otpSecret;
  final String username;

  const ValidateOtpEvent({required this.otp, required this.otpSecret, required this.username});

  @override
  List<Object> get props => [otp, otpSecret];
}
