import { AuthResponse } from 'apis/nswag';
import Language from 'features/language/types';
import { AuthActions, AuthStateType, authReducer } from 'features/auth/auth-reducer';
import { getSavedAuthInfoFromLocalStorage } from 'features/auth/utils';
import { appLang } from 'locales/i18n';
import { Dispatch, PropsWithChildren, createContext, useContext, useMemo, useReducer } from 'react';
import { LanguageActions, languageReducer } from 'features/language/language-reducer';

type InitialStateType = {
  language: Language;
  auth: AuthStateType;
};
type AppActionType = LanguageActions | AuthActions;

const initialState: InitialStateType = {
  language: {
    lang: appLang.code,
    ns: appLang.ns
  },
  auth: {
    info: null
  }
};

const AppContext = createContext<{
  state: InitialStateType;
  dispatch: Dispatch<AppActionType>;
}>({
  state: initialState,
  dispatch: () => null
});

const mainReducer = ({ language, auth }: InitialStateType, action: AppActionType) => ({
  language: languageReducer(language, action as LanguageActions),
  auth: authReducer(auth, action as AuthActions)
});

const AppProvider = ({ children }: PropsWithChildren) => {
  const initAuthInfo = getSavedAuthInfoFromLocalStorage() as AuthResponse | null;
  const [state, dispatch] = useReducer(mainReducer, {
    ...initialState,
    auth: {
      info: initAuthInfo
    }
  });
  const value = useMemo(
    () => ({
      state,
      dispatch
    }),
    [state]
  );
  return <AppContext.Provider value={value}>{children}</AppContext.Provider>;
};

const useAppContext = () => useContext(AppContext);

export { AppProvider, useAppContext };
