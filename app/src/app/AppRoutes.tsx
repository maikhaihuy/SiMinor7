import { lazy } from 'react';

const Login = lazy(() => import('features/auth/Login'));
const ForgotPassword = lazy(() => import('features/auth/ForgotPassword'));
const UpdatePassword = lazy(() => import('features/auth/UpdatePassword'));
const UserList = lazy(() => import('features/userManagement/UserList'));

export const ANONYMOUS_ROUTES = {
  Login: {
    path: 'login',
    component: Login
  },
  ForgotPassword: {
    path: 'forgot-password',
    component: ForgotPassword
  },
  UpdatePassword: {
    path: 'update-password',
    component: UpdatePassword
  }
};

export const ADMIN_APP_ROUTES = {
  User: {
    path: 'users',
    component: UserList
  }
};
