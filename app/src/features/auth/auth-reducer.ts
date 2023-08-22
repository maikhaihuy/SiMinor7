import { AuthResponse } from 'apis/nswag';
import ActionMap from 'common/types/ActionMap';
import { removeAuthInfoFromLocalStorage, saveAuthInfoToLocalStorage } from 'features/auth/utils';

export enum AuthActionTypes {
  AUTH_INFO_SET = 'AUTH_INFO_SET',
  AUTH_LOG_OUT = 'AUTH_LOG_OUT'
}

type AuthPayload = {
  [AuthActionTypes.AUTH_INFO_SET]: AuthResponse;
  [AuthActionTypes.AUTH_LOG_OUT]: undefined;
};

export type AuthStateType = {
  info: AuthResponse | null;
};

export type AuthActions = ActionMap<AuthPayload>[keyof ActionMap<AuthPayload>];

export const authReducer = (state: AuthStateType, action: AuthActions) => {
  switch (action.type) {
    case AuthActionTypes.AUTH_INFO_SET:
      saveAuthInfoToLocalStorage(action.payload);

      return {
        ...state,
        info: action.payload
      };
    case AuthActionTypes.AUTH_LOG_OUT:
      removeAuthInfoFromLocalStorage();
      return {
        info: null,
        permissions: []
      };
    default:
      return state;
  }
};
