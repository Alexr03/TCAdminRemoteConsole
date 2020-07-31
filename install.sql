create table tcmodule_terminal_scripts
(
    scriptId     int auto_increment
        primary key,
    name         text not null,
    contents     text not null,
    terminalType int  not null
);

INSERT INTO tc_site_map (page_id, module_id, parent_page_id, parent_page_module_id, category_id, url, mvc_url, controller, action, display_name, page_small_icon, panelbar_icon, show_in_sidebar, view_order, required_permissions, menu_required_permissions, page_manager, page_search_provider, cache_name) VALUES (1, '07405876-e8c2-4b24-a774-4ef57f596384', null, null, 4, '/RemoteConsole', '/RemoteConsole', 'RemoteConsole', 'Index', 'Remote Console', 'MenuIcons/Base/ServerComponents24x24.png', null, 1, null, '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', '({07405876-e8c2-4b24-a774-4ef57f596384,0,8})', null, null, 'TCAdmin.GameHosting.SDK.Objects.Service');