import { ANONYMOUS_ROUTES, ADMIN_APP_ROUTES } from 'app/AppRoutes';
import { IAppRoute } from 'common/types/Router';
import { LazyExoticComponent, Suspense } from 'react';
import { Route, BrowserRouter as Router, Routes } from 'react-router-dom';
import ScrollToTop from './ScrollToTop';
import AdminLayout from 'app/layouts/AdminLayout';
import AuthLayout from 'app/layouts/AuthLayout';
import DashboardLayout from 'app/layouts/DashboardLayout';

export const renderPage = (PageComponent: LazyExoticComponent<() => JSX.Element>) => (
  <Suspense fallback={<></>}>
    <PageComponent />
  </Suspense>
);

export const renderAppRoute = (appRoute: IAppRoute) => {
  const RouteKeys = Object.keys(appRoute) as Array<string | number>;

  return RouteKeys.length > 0
    ? RouteKeys.map((routeKey) => {
        const route = appRoute[routeKey];
        return <Route key={route.path} path={route.path} element={renderPage(route.component)} />;
      })
    : null;
};

const AppRouter = () => {
  return (
    <Router>
      <ScrollToTop>
        <Routes>
          <Route path="/admin" element={<AdminLayout />}>
            <Route element={<AuthLayout />}>
              {renderAppRoute(ANONYMOUS_ROUTES)}
            </Route>
            <Route path="dashboard" element={<DashboardLayout />}>
              {renderAppRoute(ADMIN_APP_ROUTES)}
            </Route>
          </Route>
        </Routes>
      </ScrollToTop>
    </Router>
  );
};

export default AppRouter;
